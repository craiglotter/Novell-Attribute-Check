using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace vbAccelerator.Components.Shell
{

	//As Platform SDK rather sniffily has it in the Remarks section:
	// 'The taskbar notification area is sometimes erroneously called the "tray."'
	
	#region Public Enumerations
	[Flags]	
	public enum NotifyIconBalloonIconFlags : int
	{
		/// <summary>
		/// No Icon
		/// </summary>
		NIIF_NONE  = 0x00000000,
		/// <summary>
		/// Information icon
		/// </summary>
		NIIF_INFO  = 0x00000001,
		/// <summary>
		/// Warning icon
		/// </summary>
		NIIF_WARNING = 0x00000002,
		/// <summary>
		/// Error icon
		/// </summary>
		NIIF_ERROR = 0x00000003,
		/// <summary>
		/// Don't play sound. XP only
		/// </summary>
		NIIF_NOSOUND = 0x00000010			
	}
	#endregion

	#region SysTray 
	/// <summary>
	/// Allows a form to create a taskbar notification icon which
	/// supports balloon tips under XP.
	/// </summary>
	public class SysTray : System.Windows.Forms.NativeWindow, IDisposable
	{
		#region Events
		/// <summary>
		/// Raised when the icon in the SysTray area is clicked.
		/// </summary>
		public event EventHandler Click;
		/// <summary>
		/// Raised when the icon in the SysTray area is double clicked.
		/// </summary>
		public event EventHandler DoubleClick;
		/// <summary>
		/// Occurs when the user presses the mouse button while the pointer 
		/// is over the icon in the status notification area of the taskbar.
		/// </summary>
		public event MouseEventHandler MouseDown;
		/// <summary>
		/// Occurs when the user moves the mouse while the pointer is over the 
		/// icon in the status notification area of the taskbar.
		/// </summary>
		public event MouseEventHandler MouseMove;
		/// <summary>
		/// Occurs when the user releases the mouse button while the pointer 
		/// is over the icon in the status notification area of the taskbar.
		/// </summary>
		public event MouseEventHandler MouseUp;
		/// <summary>
		/// Raised if a user selects a notify icon with the keyboard and activates 
		/// it with the space bar or ENTER key.  SE/2000 or above only.
		/// </summary>
		public event EventHandler KeySelect;
		/// <summary>
		/// Raised if a user selects a notify icon with the mouse and activates 
		/// it with the space bar or ENTER key.  SE/2000 or above only.
		/// </summary>
		public event EventHandler EnterSelect;
		/// <summary>
		/// Fired when a balloon tip is shown (balloons are queued up by the
		/// system). ME/2000 or above only.
		/// </summary>		
		public event EventHandler BalloonShow;
		/// <summary>
		/// Sent if the balloon is hidden for a reason other than it timing
		/// out or the user clicking it. ME/2000 or above only.
		/// </summary>
		public event EventHandler BalloonHide;
		/// <summary>
		/// Sent if the balloon is being hidden because it has timed out.
		/// ME/2000 or above only.
		/// </summary>
		public event EventHandler BalloonTimeOut;
		/// <summary>
		/// Send if the user dismisses the balloon by clicking it.
		/// ME/2000 or above only.
		/// </summary>
		public event EventHandler BalloonClicked;
		#endregion

		#region Private Enumerations
		[Flags]
			private enum NotifyIconDataFlags : int
		{
			/// <summary>
			/// The uCallbackMessage member is valid. 
			/// </summary>
			NIF_MESSAGE  = 0x1,
			/// <summary>
			/// The hIcon member is valid.  
			/// </summary>
			NIF_ICON = 0x2,
			/// <summary>
			/// The szTip member is valid. 
			/// </summary>
			NIF_TIP = 0x4, 
			/// <summary>
			/// The dwState and dwStateMask members are valid. 
			/// </summary>
			NIF_STATE = 0x8, 
			/// <summary>
			/// Use a balloon ToolTip instead of a standard ToolTip. The szInfo, 
			/// uTimeout, szInfoTitle, and dwInfoFlags members are valid.
			/// </summary>
			NIF_INFO = 0x10,
			/// <summary>
			/// Reserved. 
			/// </summary>
			NIF_GUID = 0x20 
		}

		[Flags]
			private enum NotifyIconDataStateFlags : int
		{
			/// <summary>
			/// The Icon is hidden
			/// </summary>
			NIS_HIDDEN = 0x00000001,
			/// <summary>
			/// The icon is shared
			/// </summary>
			NIS_SHAREDICON = 0x00000002
		}
		
		private enum NotifyIconMessages : int
		{
			/// <summary>
			/// Adds an icon to the status area. The hWnd and uID members 
			/// of the NOTIFYICONDATA structure pointed to by lpdata will be 
			/// used to identify the icon in later calls to Shell_NotifyIcon. 
			/// </summary>
			NIM_ADD = 0x00000000,
			/// <summary>
			/// Modifies an icon in the status area. Use the hWnd and uID members 
			/// of the NOTIFYICONDATA structure pointed to by lpdata to identify 
			/// the icon to be modified. 
			/// </summary>
			NIM_MODIFY = 0x00000001,
			/// <summary>
			/// Deletes an icon from the status area. Use the hWnd and uID 
			/// members of the NOTIFYICONDATA structure pointed to by lpdata 
			/// to identify the icon to be deleted. 
			/// </summary>
			NIM_DELETE = 0x00000002,
			/// <summary>
			/// Returns focus to the taskbar notification area. Taskbar icons 
			/// should use this message when they have completed their user 
			/// interface operation. For example, if the taskbar icon displays a 
			/// shortcut menu, but the user presses ESCAPE to cancel it, use 
			/// NIM_SETFOCUS to return focus to the taskbar notification area
			/// </summary>
			NIM_SETFOCUS = 0x00000003,
			/// <summary>
			/// Instructs the taskbar to behave according to the version number 
			/// specified in the uVersion member of the structure pointed to by 
			/// lpdata. This message allows you to specify whether you want the 
			/// version 5.0 behavior found on Microsoft® Windows® 2000 systems, or 
			/// that found with earlier Shell versions. The default value for 
			/// uVersion is zero, indicating that the original Windows 95 notify 
			/// icon behavior should be used.
			/// </summary>
			NIM_SETVERSION = 0x00000004
		}

		#endregion

		#region Private Structures

		[StructLayoutAttribute(LayoutKind.Sequential, Pack=4, Size=0, CharSet=CharSet.Auto)]
		private struct NOTIFYICONDATA 
		{ 
			/// <summary>
			/// Size of structure.  Note for pre-Windows 2000 systems the
			/// szTip member is 64 characters long and ends the structure.
			/// </summary>
			public int cbSize; 
			/// <summary>
			/// Window handle to sed messages to
			/// </summary>
			public IntPtr hWnd; 
			/// <summary>
			/// Application-defined identifier for the taskbar item.  A
			/// single hWnd can have multiple taskbar icons using this
			/// member.
			/// </summary>
			public int uID; 
			/// <summary>
			/// Flags that indicate which of the other members contain valid data
			/// </summary>
			[MarshalAs(UnmanagedType.U4)] public NotifyIconDataFlags uFlags; 
			/// <summary>
			/// Application-defined message identifier. The system uses this 
			/// identifier to send notifications to the window identified in hWnd. 
			/// These notifications are sent when a mouse event occurs in the 
			/// bounding rectangle of the icon, or when the icon is selected or activated 
			/// with the keyboard. The wParam parameter of the message contains the 
			/// identifier of the taskbar icon in which the event occurred. 
			/// The lParam parameter holds the mouse or keyboard message associated 
			/// with the event. For example, when the pointer moves over a taskbar icon, 
			/// lParam is set to WM_MOUSEMOVE. 
			/// </summary>
			public int uCallbackMessage; 
			/// <summary>
			/// Handle to the icon to be added, modified or deleted.
			/// </summary>
			public IntPtr hIcon; 
			/// <summary>
			/// Tool tip string
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=128)] public string szTip;
			/// <summary>
			/// State of the icon
			/// </summary>
			[MarshalAs(UnmanagedType.U4)] public NotifyIconDataStateFlags dwState; 
			/// <summary>
			/// A mask to determine which items are retrieved/set by the dwState
			/// member.
			/// </summary>
			[MarshalAs(UnmanagedType.U4)] public NotifyIconDataStateFlags dwStateMask;
			/// <summary>
			/// Text for a balloon tool-tip.
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=256)] public string szInfo; 
			/// <summary>
			/// When used as timeout:
			/// The timeout value, in milliseconds, for a balloon ToolTip. The 
			/// system enforces minimum and maximum timeout values. uTimeout values that 
			/// are too large are set to the maximum value and values that are too small 
			/// default to the minimum value. The system minimum and maximum timeout 
			/// values are currently set at 10 seconds and 30 seconds, respectively.
			/// When used as version:
			/// Specifies whether the Shell notify icon interface should use Windows 95 or 
			/// Windows 2000 behavior.  Allowable values are 0 or NOTIFYICON_VERSION
			/// </summary>
			public int uTimeoutOrVersion; 
			/// <summary>
			/// Title for the balloon tip.
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=64)] public string szInfoTitle; 
			/// <summary>
			/// Flags for controlling the icon in the balloon tip
			/// </summary>
			[MarshalAs(UnmanagedType.U4)] public NotifyIconBalloonIconFlags dwInfoFlags; 
			/// <summary>
			/// Reserved. XP only.
			/// </summary>
			public Guid guidItem;
		}
		private const int NOTIFYICON_VERSION = 3;
		private const int NOTIFYICONDATAA_V1_SIZE = 88;
		private const int NOTIFYICONDATAA_V2_SIZE = 488;

		private const int WM_DESTROY = 0x2;

		private const int WM_MOUSEMOVE = 0x200;
		private const int WM_LBUTTONDOWN = 0x201;
		private const int WM_LBUTTONUP = 0x202;
		private const int WM_LBUTTONDBLCLK = 0x203;
		private const int WM_RBUTTONDOWN = 0x204;
		private const int WM_RBUTTONUP = 0x205;
		private const int WM_RBUTTONDBLCLK = 0x206;

		private const int WM_USER = 0x400;

		
		private const int NIN_SELECT = WM_USER;
		private const int NINF_KEY = 0x1;
		private const int NIN_KEYSELECT = (NIN_SELECT | NINF_KEY);
		private const int NIN_BALLOONSHOW = (WM_USER + 2);
		private const int NIN_BALLOONHIDE = (WM_USER + 3);
		private const int NIN_BALLOONTIMEOUT = (WM_USER + 4);
		private const int NIN_BALLOONUSERCLICK = (WM_USER + 5);

		/// <summary>
		/// For sending private messages within a window class,
		/// an application can use any integer in the range WM_USER through 
		/// 0x7FFF
		/// </summary>		
		private const int NOTIFYMESSAGE = WM_USER + 0x4FB5;
		
		#endregion

		#region UnManagedMethods

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private extern static int Shell_NotifyIcon(
			[MarshalAs(UnmanagedType.U4)] NotifyIconMessages dwMessage,
			ref NOTIFYICONDATA lpData);

		/* Could use this to generate our message, but it adds
		 * overhead because the message is globally registered...
		[DllInport("user32.dll", CharSet=CharSet.Auto)]
		private extern static IntPtr RegisterWindowMessage(
			[MarshalAs(UnmanagedType.LPTStr)] string lpString   // message string
			);
		*/

		[DllImport("user32.dll")]
		private extern static int DestroyIcon(
			IntPtr hIcon);

		[DllImport("user32.dll")]
		private extern static int GetCursorPos(
			ref System.Drawing.Point lpPoint);

		[DllImport("user32.dll")]
		private extern static int TrackPopupMenu(
			IntPtr hMenu, 
			int wFlags, 
			int x, 
			int y, 
			int nReserved, 
			IntPtr hwnd, 
			ref System.Drawing.Rectangle lprc);

		[DllImport("user32.dll")]
		private extern static int SetForegroundWindow (
			IntPtr hwnd );

		private const int TPM_RETURNCMD = 0x0100;

		[DllImport("comctl32.dll")]
		private extern static IntPtr ImageList_GetIcon(
			IntPtr himl,
			int i,  
			int flags 		
			);

		#endregion

		#region Unmanaged Icon
		private class UnManagedIcon : IDisposable
		{
			private IntPtr hIcon = IntPtr.Zero;

			public IntPtr Handle
			{
				get
				{
					return this.hIcon;
				}
				set
				{
					this.hIcon = value;
				}
			}

			public UnManagedIcon(IntPtr hIcon)
			{
				this.hIcon = hIcon;
			}
			public UnManagedIcon()
			{
			}

			public void Dispose()
			{				
				if (hIcon != IntPtr.Zero)
				{
					DestroyIcon(hIcon);
				}
				hIcon = IntPtr.Zero;
			}
		}
		#endregion

		#region Member Variables
		/// <summary>
		/// The Form to associate the SysTray with
		/// </summary>
		private System.Windows.Forms.Form ownerForm = null;
		/// <summary>
		/// The ImageList to use
		/// </summary>
		private System.Windows.Forms.ImageList ilsIcons = null;
		/// <summary>
		/// The context menu to use
		/// </summary>
		private System.Windows.Forms.ContextMenu mnuContext = null;
		/// <summary>
		/// The icon index to show
		/// </summary>
		private int iconIndex = 0;
		/// <summary>
		/// The Unmanaged version of the icon extracted from the 
		/// image list
		/// </summary>
		private UnManagedIcon icon = null;
		/// <summary>
		/// The ToolTip to show
		/// </summary>
		private string toolTip = "";
		/// <summary>
		/// Whether we're currently in the SysTray or not
		/// </summary>
		private bool inSysTray = false;
		/// <summary>
		/// Whether we were installed in the SysTray before the
		/// handle changed.
		/// </summary>
		private bool wasInSysTray = false;
		#endregion

		#region Constructor, Dispose
		/// <summary>
		/// Creates a new instance of the SysTray object, associating
		/// it with the top level Form.
		/// </summary>
		/// <param name="form">The top level Form to
		/// associate with</param>
		public SysTray(System.Windows.Forms.Form form)
		{
			this.ownerForm = form;
			this.AssignHandle(this.ownerForm.Handle);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		public void Dispose()
		{
			showInSysTray(false);
			if (this.Handle != IntPtr.Zero)
			{
				this.ReleaseHandle();
			}
		}

		#endregion

		#region Methods and Properties
		/// <summary>
		/// Gets/sets the ImageList used as a source of icons
		/// in the SysTray
		/// </summary>
		public System.Windows.Forms.ImageList IconImageList
		{
			get
			{
				return this.ilsIcons;
			}
			set
			{
				this.ilsIcons = value;
			}
		}
		/// <summary>
		/// Gets/sets the context menu to show in response 
		/// to a SysTray click
		/// </summary>
		public System.Windows.Forms.ContextMenu Menu
		{
			get
			{
				return this.mnuContext;
			}
			set
			{
				this.mnuContext = value;
			}
		}

		/// <summary>
		/// Gets/sets the IconIndex to use in the SysTray
		/// </summary>
		public int IconIndex
		{
			get
			{
				return this.iconIndex;
			}
			set
			{
				this.iconIndex = value;
				if (this.inSysTray)
				{
					NOTIFYICONDATA ids = newNotifyIconData();
					setIcon(ref ids);
					int ret = Shell_NotifyIcon(
						NotifyIconMessages.NIM_MODIFY,
						ref ids);				
				}
			}
		}

		/// <summary>
		/// Gets/sets the tool tip text to display when the
		/// user hovers above the item
		/// </summary>
		public string ToolTipText
		{
			get
			{
				return this.toolTip;
			}
			set
			{
				this.toolTip = value;
				if (this.inSysTray)
				{
					NOTIFYICONDATA ids = newNotifyIconData();
					setMessage(ref ids);
					int ret = Shell_NotifyIcon(
						NotifyIconMessages.NIM_MODIFY,
						ref ids);			
				}
			}
		}
	
		/// <summary>
		/// Shows a balloon tip over the item with no icon, no title
		/// and for the maximum timeout value.  ME/2000 or above only.
		/// </summary>
		/// <param name="message">Balloon tip message to display (max 256 chars)</param>
		public void ShowBalloonTip(
			string message)
		{
			ShowBalloonTip(
				message,
				NotifyIconBalloonIconFlags.NIIF_NONE,
				"",
				30000);
		}
		/// <summary>
		/// Shows a balloon tip over the item with the specified icon
		/// and message for the maximum timeout value.  ME/2000 or above only.
		/// </summary>
		/// <param name="message">Balloon tip message to display (max 256 chars)</param>
		/// <param name="icon">Icon to show</param>
		public void ShowBalloonTip(
			string message,
			NotifyIconBalloonIconFlags icon
			)
		{
			ShowBalloonTip(
				message,
				icon,
				"",
				100000);
		}
		/// <summary>
		/// Shows a balloon tip over the item with the specified icon,
		/// message and title, for the maximum timeout value.  ME/2000 or above only.
		/// </summary>
		/// <param name="message">Balloon tip message to display (max 256 chars)</param>
		/// <param name="icon">Icon to show</param>
		/// <param name="title">Title of the balloon tip (max 64 chars)</param>
		public void ShowBalloonTip(
			string message,
			NotifyIconBalloonIconFlags icon,
			string title
			)
		{
			ShowBalloonTip(
				message,
				icon,
				title,
				100000);
		}
		/// <summary>
		/// Shows a balloon tip over the item with the specified icon,
		/// message and title for the specified timeout interval.  ME/2000 or above only.
		/// </summary>
		/// <param name="message">Balloon tip message to display (max 256 chars)</param>
		/// <param name="icon">Icon to show</param>
		/// <param name="title">Title of balloon tip (max 64 chars)</param>
		/// <param name="timeOut">Timeout in milliseconds.  The system restricts timeouts
		/// to the range 10,000 - 30,000 ms.</param>
		public void ShowBalloonTip(
			string message,
			NotifyIconBalloonIconFlags icon,
			string title,
			int timeOut
			)
		{
			if (this.inSysTray)
			{
				// show the balloon tip:
				NOTIFYICONDATA ids = newNotifyIconData();
				ids.uFlags = NotifyIconDataFlags.NIF_INFO;
				ids.szInfo  = message;
				ids.szInfoTitle = title;
				ids.dwInfoFlags = icon;
				ids.uTimeoutOrVersion = timeOut;

				int ret = Shell_NotifyIcon(
					NotifyIconMessages.NIM_MODIFY,
					ref ids);				
			}
			else
			{
				// should throw exception?
			}
		}
		/// <summary>
		/// Gets/sets whether the item is shown in the SysTray or not.
		/// </summary>
		public bool ShowInSysTray
		{
			get
			{
				return this.inSysTray;
			}
			set
			{
				showInSysTray(value);
			}
		}
		/// <summary>
		/// Sets focus to your icon in the SysTray.  Should be called
		/// once you've completed a UI operation such as a menu.
		/// ME/2000 or above only.
		/// </summary>
		public void SetFocus()
		{
			NOTIFYICONDATA ids = newNotifyIconData();
			int ret = Shell_NotifyIcon(
				NotifyIconMessages.NIM_SETFOCUS,
				ref ids);				
			
		}
		#endregion

		#region Private helper routines
		private NOTIFYICONDATA newNotifyIconData()
		{
			return this.newNotifyIconData(false);
		}

		private NOTIFYICONDATA newNotifyIconData(bool useLastHandle)
		{
			NOTIFYICONDATA ids = new NOTIFYICONDATA();
				
			ids.uCallbackMessage = NOTIFYMESSAGE;
			ids.hWnd = this.Handle;
			ids.uID = 1;			

			if (Environment.OSVersion.Version.Major >= 5)
			{
				ids.cbSize = Marshal.SizeOf(ids);
				ids.uTimeoutOrVersion = NOTIFYICON_VERSION;
			}
			else
			{
				ids.cbSize = NOTIFYICONDATAA_V1_SIZE;
			}
			return ids;
		}

		private void setIcon(ref NOTIFYICONDATA ids)
		{
			if (this.ilsIcons != null)
			{
				// Let's see if we can get the icon:
				if (icon != null)
				{
					icon.Dispose();
				}
				IntPtr hIcon = ImageList_GetIcon(
					ilsIcons.Handle,
					iconIndex,
					0);
				if (hIcon != IntPtr.Zero)
				{
					icon = new UnManagedIcon(hIcon);
					ids.uFlags |= NotifyIconDataFlags.NIF_ICON;
					ids.hIcon = icon.Handle;
				}
					
			}
		}

		private void setMessage(ref NOTIFYICONDATA ids)
		{
			if (this.toolTip.Length > 0)
			{
				ids.uFlags |= NotifyIconDataFlags.NIF_TIP;
				ids.szTip = this.toolTip;
			}
		}

		private void showInSysTray(bool state)
		{
			NOTIFYICONDATA ids = newNotifyIconData(!state);
			ids.uFlags = NotifyIconDataFlags.NIF_MESSAGE;

			if (state)
			{	
				if (!this.inSysTray)
				{
					setIcon(ref ids);
					setMessage(ref ids);

					// add to systray:
					int ret = Shell_NotifyIcon(
						NotifyIconMessages.NIM_ADD,
						ref ids);
					// set version:
					if (ids.uTimeoutOrVersion == NOTIFYICON_VERSION)
					{
						ret = Shell_NotifyIcon(
							NotifyIconMessages.NIM_SETVERSION,
							ref ids);
					}
				}
			}
			else
			{
				if (this.inSysTray)
				{
					// remove from systray
					Shell_NotifyIcon(
						NotifyIconMessages.NIM_DELETE,
						ref ids);
					if (icon != null)
					{
						icon.Dispose();
					}
				}
			}
			this.inSysTray = state;
			this.wasInSysTray = this.inSysTray;

		}
		private MouseEventArgs getMouseEventArgs(
			MouseButtons button
			)
		{
			Point pt = new Point(0,0);
			GetCursorPos(ref pt);
			int delta = 0;
			int clicks = 0;
			MouseEventArgs e = new MouseEventArgs(
				button,
				clicks,
				pt.X, pt.Y,
				delta);
			return e;
		}
		#endregion

		#region Message Processing
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			if (m.Msg == NOTIFYMESSAGE)
			{
				// process SysTray notification
				switch ((int)m.LParam)
				{
					case WM_MOUSEMOVE:
						if (MouseMove != null)
						{
							MouseEventArgs e = getMouseEventArgs(MouseButtons.None);
							MouseMove(this, e);
						}
						break;
					case WM_LBUTTONUP:
						if (MouseUp != null)
						{
							MouseEventArgs e = getMouseEventArgs(MouseButtons.Left);
							MouseUp(this, e);
						}
						if (Click != null)
						{
							Click(this, null);
						}
						break;
					case WM_LBUTTONDOWN:
						if (MouseDown != null)
						{
							MouseEventArgs e = getMouseEventArgs(MouseButtons.Left);
							MouseDown(this, e);
						}
						break;
					case WM_LBUTTONDBLCLK:
						if (DoubleClick != null)
						{
							DoubleClick(this, null);
						}
						break;
					case WM_RBUTTONDOWN:
						if (MouseDown != null)
						{
							MouseEventArgs e = getMouseEventArgs(MouseButtons.Right);
							MouseDown(this, e);
						}
						break;
					case WM_RBUTTONUP:
						if (MouseUp != null)
						{
							MouseEventArgs e = getMouseEventArgs(MouseButtons.Right);
							MouseUp(this, e);
						}

						// if a context menu is set then show it
						if (mnuContext != null)
						{
							System.Drawing.Point pos = new System.Drawing.Point(0,0);
							GetCursorPos(ref pos);							
							IntPtr hMenu = mnuContext.Handle;
							if (hMenu != IntPtr.Zero)
							{
								Rectangle rc = new Rectangle(0,0,0,0);
								SetForegroundWindow(this.Handle);
								TrackPopupMenu(
									hMenu, 
									0, 
									pos.X, pos.Y, 
									0, 
									this.Handle, 
									ref rc);
								SetFocus();
							}
						}
						
						if (Click != null)
						{
							Click(this, null);
						}
						break;

					case WM_RBUTTONDBLCLK:
						if (DoubleClick != null)
						{
							this.DoubleClick(this, null);
						}
						break;

					case NIN_KEYSELECT:
						if (KeySelect != null)
						{
							KeySelect(this, null);
						}
						break;

					case NIN_SELECT:
						if (EnterSelect != null)
						{
							EnterSelect(this, null);
						}
						break;

					case NIN_BALLOONSHOW:
						if (BalloonShow != null)
						{							
							BalloonShow(this, null);
						}
						break;

					case NIN_BALLOONHIDE:
						if (BalloonHide != null)
						{
							BalloonHide(this, null);
						}
						break;

					case NIN_BALLOONTIMEOUT:
						if (BalloonTimeOut != null)
						{
							BalloonTimeOut(this, null);
						}
						break;

					case NIN_BALLOONUSERCLICK:
						if (BalloonClicked != null)
						{
							BalloonClicked(this, null);
						}
						break;
				}
			}
			// Added 2003-09-27 to prevent problem with
			// handle changing
			else if (m.Msg == WM_DESTROY)
			{
				// remove.
				this.wasInSysTray = this.inSysTray;
				showInSysTray(false);
			}
			base.DefWndProc(ref m);
		}
		#endregion

		private void SysTray_Load(object sender, System.EventArgs e)
		{
		
		}
	}
	#endregion
}
