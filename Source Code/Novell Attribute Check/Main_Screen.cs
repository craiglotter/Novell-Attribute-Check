using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Novell.Directory.Ldap;


namespace Novell_Attribute_Check
{
	/// <summary>
	/// Summary description for Main_Screen.
	/// </summary>
	public class Main_Screen : System.Windows.Forms.Form
	{
		private System.Windows.Forms.RichTextBox label1;
		private System.Windows.Forms.TextBox Username;
		private System.Windows.Forms.ComboBox Context;
		private System.Windows.Forms.TextBox Password;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label Statusbar;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Main_Screen()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Main_Screen));
			this.label1 = new System.Windows.Forms.RichTextBox();
			this.Username = new System.Windows.Forms.TextBox();
			this.Context = new System.Windows.Forms.ComboBox();
			this.Password = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.Statusbar = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(24, 136);
			this.label1.Name = "label1";
			this.label1.ReadOnly = true;
			this.label1.Size = new System.Drawing.Size(400, 208);
			this.label1.TabIndex = 5;
			this.label1.Text = "";
			// 
			// Username
			// 
			this.Username.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Username.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Username.ForeColor = System.Drawing.Color.Black;
			this.Username.Location = new System.Drawing.Point(24, 48);
			this.Username.Name = "Username";
			this.Username.Size = new System.Drawing.Size(200, 20);
			this.Username.TabIndex = 1;
			this.Username.Text = "";
			// 
			// Context
			// 
			this.Context.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Context.ForeColor = System.Drawing.Color.Black;
			this.Context.Items.AddRange(new object[] {
														 "com.main.uct",
														 "Staff.com.main.uct",
														 "Students.com.main.uct"});
			this.Context.Location = new System.Drawing.Point(24, 96);
			this.Context.Name = "Context";
			this.Context.Size = new System.Drawing.Size(200, 21);
			this.Context.Sorted = true;
			this.Context.TabIndex = 3;
			// 
			// Password
			// 
			this.Password.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Password.ForeColor = System.Drawing.Color.Black;
			this.Password.Location = new System.Drawing.Point(24, 72);
			this.Password.Name = "Password";
			this.Password.PasswordChar = '*';
			this.Password.Size = new System.Drawing.Size(200, 20);
			this.Password.TabIndex = 2;
			this.Password.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(232, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "LOGIN NAME";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(232, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "NETWORK PASSWORD";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Black;
			this.label4.Location = new System.Drawing.Point(232, 96);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "USER CONTEXT";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Black;
			this.label5.Location = new System.Drawing.Point(24, 12);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(400, 24);
			this.label5.TabIndex = 7;
			this.label5.Text = "TO VIEW THE ATTRIBUTE SET OF YOUR UCT NOVELL ACCOUNT, ENTER YOUR LOGIN DETAILS BE" +
				"LOW (REMEMBER TO CHOOSE THE CORRECT CONTEXT) AND HIT THE PROCESS BUTTON.";
			// 
			// panel1
			// 
			this.panel1.ForeColor = System.Drawing.Color.Black;
			this.panel1.Location = new System.Drawing.Point(23, 96);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(202, 23);
			this.panel1.TabIndex = 8;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.ForeColor = System.Drawing.Color.Black;
			this.panel2.Location = new System.Drawing.Point(23, 135);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(402, 210);
			this.panel2.TabIndex = 9;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.LightGray;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.Black;
			this.button1.Location = new System.Drawing.Point(361, 108);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 20);
			this.button1.TabIndex = 4;
			this.button1.Text = "PROCESS";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Statusbar
			// 
			this.Statusbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Statusbar.ForeColor = System.Drawing.Color.Firebrick;
			this.Statusbar.Location = new System.Drawing.Point(24, 382);
			this.Statusbar.Name = "Statusbar";
			this.Statusbar.Size = new System.Drawing.Size(400, 9);
			this.Statusbar.TabIndex = 11;
			this.Statusbar.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Gray;
			this.label6.Location = new System.Drawing.Point(24, 372);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(400, 9);
			this.label6.TabIndex = 12;
			this.label6.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.Gray;
			this.label7.Location = new System.Drawing.Point(24, 362);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(400, 9);
			this.label7.TabIndex = 13;
			this.label7.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.ForeColor = System.Drawing.Color.Gray;
			this.label8.Location = new System.Drawing.Point(24, 352);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(400, 9);
			this.label8.TabIndex = 14;
			this.label8.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// Main_Screen
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(450, 396);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.Statusbar);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Password);
			this.Controls.Add(this.Context);
			this.Controls.Add(this.Username);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.ForeColor = System.Drawing.Color.Black;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(458, 430);
			this.MinimumSize = new System.Drawing.Size(458, 430);
			this.Name = "Main_Screen";
			this.Text = "Novell Attribute Check 20050719.3";
			this.Load += new System.EventHandler(this.Main_Screen_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Main_Screen());
		}

		private void Main_Screen_Load(object sender, System.EventArgs e)
		{
			try
			{
				Context.SelectedIndex = 1;
			}
			catch(System.Exception except)
			{
				Error_Handler(except);
			}
		}

		private void Error_Handler(System.Exception except)
		{
			label1_Message("--------------\nError Encountered:\n" + except.ToString());
			Statusbar_Message("Error Encountered");
		}

		private void Error_Handler(string except)
		{
			label1_Message("--------------\nError Encountered:\n" + except);
			Statusbar_Message("Error Encountered");
		}

		private void Statusbar_Message(string message)
		{
			label8.Text = label7.Text;
			label8.Refresh();
			label7.Text = label6.Text;
			label7.Refresh();
			label6.Text = Statusbar.Text;
			label6.Refresh();
			message = message.ToUpper();
			Statusbar.Text = message;
			Statusbar.Refresh();
		}
	
		private void label1_Message(string message)
		{
			if (label1.TextLength > 0)
				label1.Text = label1.Text + "\n" + message;
			else
				label1.Text = label1.Text  + message;
			label1.Refresh();
		}
		
		private int ldap_check(int serveruse)
		{
			try
			{
				if (serveruse == 1)
			Statusbar_Message("Retrieving username input");
				if (Username.Text.Equals("") == true)
				{
					Error_Handler("Input Error: Username field has been left blank.");
					Statusbar_Message("Operation failed");
					return 0;
				}
if (serveruse == 1)
				Statusbar_Message("Retrieving password input");
				string userPasswd = Password.Text;
				
				if (Password.Text.Equals("") == true)
				{
					Error_Handler("Input Error: Password field has been left blank.");
					Statusbar_Message("Operation failed");
					return 0;
				}	

				if (Context.SelectedIndex < 0)
				{
					Error_Handler("Invalid Context selected from the list.");
					Statusbar_Message("Operation failed");
					return 0;
				}	

				string tdn = "";
				string[] tempdn = Context.Items[Context.SelectedIndex].ToString().Split('.');
				System.Collections.IEnumerator runner = tempdn.GetEnumerator();
				while(runner.MoveNext())
				{
					if (runner.Current.Equals("uct") == false)
						tdn = tdn + "OU="+ runner.Current + ",";
					else
						tdn = tdn + "O="+ runner.Current + ",";
				}
				tdn = tdn.Remove(tdn.Length-1,1);
				string userDN = "CN=" + Username.Text.ToUpper() + "," + tdn;
				if (serveruse == 1)
				Statusbar_Message("Setting DN string as " + userDN);

			
				
				string ldapHost;
				int ldapPort;
				int ldapVersion = LdapConnection.Ldap_V3;
				if (serveruse == 1)
				{
					ldapHost = "rep1.uct.ac.za";
					ldapPort = LdapConnection.DEFAULT_PORT;
				}
				else
				{
					ldapHost = "rep2.uct.ac.za";
					ldapPort = LdapConnection.DEFAULT_PORT;
				}
				Statusbar_Message("Setting LDAP server as " + ldapHost + ":" + ldapPort);

				Statusbar_Message("Attempting to bind " + Username.Text + " to " + ldapHost + ":" + ldapPort);
				// Creating an LdapConnection instance
				LdapConnection ldapConn= new LdapConnection();
				try				
				{
					//Connect function will create a socket
					//ldapConn.SecureSocketLayer = true;
					ldapConn.Connect(ldapHost,ldapPort);
					//Bind function will Bind the user
					//ldapConn.startTLS();
					ldapConn.Bind(ldapVersion,userDN,userPasswd);
					//ldapConn.stopTLS();
								}
				catch(System.Exception except)
				{
					Error_Handler(except);
					if (serveruse == 1)
					{
						Statusbar_Message("Contacting Secondary LDAP server");
						ldap_check(2);
						return 0;
					}
					else
					{
						Statusbar_Message("Operation failed");
						return 0;
						
					}
				}

				
				//LdapAttribute attr = new LdapAttribute("userPassword", userPasswd);
				//bool correct = ldapConn.Compare(userDN, attr);
				//label1_Message("Password Verify: " + correct);


				Statusbar_Message("Retrieving User Account Attributes");
				// Searches in the Marketing container and return all child entries just below this
				//container i.e Single level search
				LdapSearchQueue queue=ldapConn.Search(tdn,LdapConnection.SCOPE_SUB,
					//"objectClass=*"
					"CN=" + Username.Text.ToUpper(),				
					null,		
					false,
					(LdapSearchQueue) null,
					(LdapSearchConstraints) null );

				LdapMessage message;
				while ((message = queue.getResponse()) != null)
				
				{
					if (message is LdapSearchResult)
					{
						LdapEntry entry = ((LdapSearchResult) message).Entry;
						label1_Message("------------------------------------------------\n ENTRY:\n------------------------------------------------");
						label1_Message(" " + entry.DN );
						label1_Message("------------------------------------------------\n ATTRIBUTES:\n------------------------------------------------");
						LdapAttributeSet attributeSet =  entry.getAttributeSet();
						
						System.Collections.IEnumerator ienum = attributeSet.GetEnumerator();
						while(ienum.MoveNext())
						{
							LdapAttribute attribute=(LdapAttribute)ienum.Current;
							string attributeName = attribute.Name;
							//if (attributeName.Equals("groupMembership") == true)
						{
							System.Collections.IEnumerator ienum2 = attribute.StringValues;
								
							while(ienum2.MoveNext())
							{
								//string gr =(string)ienum.Current;
								//label1.Text = label1.Text + " \n " +  attributeName + " value:" + gr;
								label1_Message(" Attribute: " + attributeName + "\n Value: " + ienum2.Current.ToString() + "");
							}
						}
							
						}
					}
				}

Statusbar_Message("Disconnecting from LDAP server");

				ldapConn.Disconnect();
				Statusbar_Message("Operation successfully completed");
				return 1;
			}
			catch(System.Exception except)
			{
								Error_Handler(except);
				Statusbar_Message("Operation failed");
			}
			return 0;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			label8.Text = "";
			label8.Refresh();
			label7.Text = "";
			label7.Refresh();
			label6.Text = "";
			label6.Refresh();
			Statusbar.Text = "";
			Statusbar.Refresh();
			label1.Text = "";
			label1.Refresh();
			ldap_check(1);
		}


	}
}
