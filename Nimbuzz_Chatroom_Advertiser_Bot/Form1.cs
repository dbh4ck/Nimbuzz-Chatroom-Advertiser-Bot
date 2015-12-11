using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using agsXMPP;
using System.Diagnostics;
using agsXMPP.Exceptions;
using agsXMPP.Collections;
using agsXMPP.Util;
using agsXMPP.protocol.client;
using agsXMPP.protocol.iq;
using agsXMPP.protocol.iq.roster;
using agsXMPP.Xml.Dom;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.ObjectModel;
using System.Web;
using System.IO;


namespace Nimbuzz_Chatroom_Advertiser_Bot
{
    public partial class Form1 : Form
    {
        XmppClientConnection bcon = new XmppClientConnection();
        public Form1()
        {
            InitializeComponent();
            bcon.OnLogin += new ObjectHandler(bcon_onlogin);
            bcon.OnAuthError += new XmppElementHandler(bcon_onerror);
            bcon.OnPresence += new agsXMPP.protocol.client.PresenceHandler(bcon_onpresence);
            bcon.OnMessage += new agsXMPP.protocol.client.MessageHandler(bcon_onmsg);
            bcon.OnReadXml += new XmlHandler(bcon_OnReadXml);
         // bcon.OnWriteXml += new XmlHandler(XmppCon_OnWriteXml);
            bcon.OnRosterItem += new XmppClientConnection.RosterHandler(bcon_OnRosterItem);
            bcon.OnRosterStart += new ObjectHandler(bcon_OnRosterStart);
            bcon.OnRosterEnd += new ObjectHandler(bcon_OnRosterEnd);
            bcon.OnIq += new IqHandler(bcon_oniq);
        }

     private void bcon_OnRosterItem(object sender, agsXMPP.protocol.iq.roster.RosterItem item)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new XmppClientConnection.RosterHandler(bcon_OnRosterItem), new object[] { sender, item });
                return;
            }
            listBox2.Items.Add(String.Format("{0}", item.Jid.User));
            listBox2.SelectedIndex = listBox2.Items.Count - 1;
        }


      private void bcon_OnRosterStart(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(bcon_OnRosterStart), new object[] { sender });
                return;
            }
        }

      private void bcon_OnRosterEnd(object sender)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(bcon_OnRosterEnd), new object[] { sender });
                return;
            }
            bcon.SendMyPresence();
        }

     
        private string Username;

       private void button1_Click(object sender, EventArgs e)
        
            {
                listBox1.Items.Add("Please wait...");
                bcon.Server = "nimbuzz.com";
                bcon.ConnectServer = "o.nimbuzz.com";
                bcon.Open(textBox1.Text, textBox2.Text, textBox3.Text, 50);
                bcon.Username = textBox1.Text;
                bcon.Password = textBox2.Text;
                bcon.Resource = textBox3.Text;
                bcon.Priority = 10;
                bcon.Status = "Online via DBuzz :)";
                bcon.Port = 5222;
                bcon.AutoAgents = false;
                bcon.AutoResolveConnectServer = true;
                bcon.UseCompression = false;
                bcon.AutoRoster = true;
                Username = textBox1.Text;
                
            }

        private void bcon_OnReadXml(object sender, string xml)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new agsXMPP.XmlHandler(bcon_OnReadXml), new object[] { sender, xml });
                return;
            }

            groupBox4.Text = "Total Users in this Chatroom: [" + listBox3.Items.Count + "]";
            groupBox6.Text = "Contact list: [" + listBox2.Items.Count + "]";
            
            }

        void bcon_onerror(object sender, agsXMPP.Xml.Dom.Element e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new XmppElementHandler(bcon_onerror), new object[] { sender, e });
                return;
            }

            MessageBox.Show("Yu have entered a wrong username or password", "Attention!");

            listBox1.Items.Add("Wrong username or password");
            textBox1.BackColor = Color.Red;
            textBox2.BackColor = Color.Red;

        }


        void bcon_onlogin(object sender)
        {

            if (InvokeRequired)
            {
                BeginInvoke(new ObjectHandler(bcon_onlogin), new object[] { sender });
                return;
            }
            listBox1.Items.Clear();
            listBox1.Items.Add("✓ Connected ✓");
            textBox1.BackColor = Color.Green;
            textBox2.BackColor = Color.Green;
            textBox3.BackColor = Color.Green;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            bcon.SocketDisconnect();
            textBox1.BackColor = Color.Yellow;
            textBox2.BackColor = Color.Yellow;
            textBox3.BackColor = Color.Yellow;
            richTextBox1.Clear();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }



        void bcon_onmsg(object sender, agsXMPP.protocol.client.Message msg)
        {
           
            if (msg.Type == MessageType.error || msg.Body == null)
                return;
            if (InvokeRequired)
            {
                BeginInvoke(new agsXMPP.protocol.client.MessageHandler(bcon_onmsg), new object[] { sender, msg });
                return;
            }

            if (msg.Type == MessageType.groupchat)
            {
                if (msg.From.Resource == "admin")
                {
                    pictureBox1.Load(msg.Body.Replace("Enter the right answer to start chatting.", ""));
                    // textBox5.Text = msg.Body.Replace("Enter the right answer to start chatting.", "");
                }
            }

       }



        private void bcon_oniq(object sender, agsXMPP.protocol.client.IQ iq)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new IqHandler(bcon_oniq), new object[] { sender, iq });
                return;
            }

        }

        
        private void button5_Click(object sender, EventArgs e)
        {   
           //if (richTextBox1.Text != string.Empty)
           // {
                //agsXMPP.Jid JID = new Jid("listBox2.Items.Add");
                //agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
                //msg.Type = agsXMPP.protocol.client.MessageType.chat;
                //msg.To = JID;
                //msg.Body = richTextBox1.Text + DateTime.Now.ToString();
               // msg.To = JID;
                //msg.Body = "hi how are you? message" + DateTime.Now.ToString();
                   
          // }
            
            //richTextBox1.Clear();
            //bcon.Send("<message xmlns='jabber:client' to='" + textBox4.Text + "@conference.nimbuzz.com/" + listBox3.Items[a].ToString() + "' type='chat' id=''><body>" + richTextBox1.Text + "</body><active xmlns='http://jabber.org/protocol/chatstates' /><x xmlns='jabber:x:event'><composing /></x></message>");
            

           for (int a = 0; a < listBox3.Items.Count; a++)
            {
                
               bcon.Send("<message xmlns='jabber:client' to='" + textBox4.Text + "@conference.nimbuzz.com/" +listBox3.Items[a].ToString()+ "' type='chat' id=''><body>" + richTextBox1.Text + "</body><active xmlns='http://jabber.org/protocol/chatstates' /><x xmlns='jabber:x:event'><composing /></x></message>");
            
            }

            richTextBox1.Clear();
           
        }


        private Jid Roomjid;
        agsXMPP.protocol.x.muc.MucManager manager;
        private void button3_Click_1(object sender, EventArgs e)
        {
             manager = new agsXMPP.protocol.x.muc.MucManager(bcon);
             Jid room = new Jid(textBox4.Text + "@conference.nimbuzz.com");
             manager.JoinRoom(room, Username);
             manager.AcceptDefaultConfiguration(room);
             Roomjid = room;
            
            //you can also use xml to join room
            //try
            //{
             //   bcon.Send("<presence to='" + textBox4.Text + "@conference.nimbuzz.com/" + textBox1.Text + "' xml:lang='en'><x xmlns='http://jabber.org/protocol/muc' /></presence>");
           // }
          // catch { }
           
        }


        void bcon_onpresence(object sender, agsXMPP.protocol.client.Presence pres)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new agsXMPP.protocol.client.PresenceHandler(bcon_onpresence), new object[] { sender, pres });
                return;
            }

              

                if (pres.From.Server.StartsWith("conference"))
                {
                    if (pres.MucUser != null)
                    {
                        if (pres.MucUser.Item.Affiliation == agsXMPP.protocol.x.muc.Affiliation.owner)
                       
                         {
                            listBox3.Items.Add(pres.From.Resource);
                         } 

                        if (pres.MucUser.Item.Affiliation == agsXMPP.protocol.x.muc.Affiliation.admin)
                        {
                            listBox3.Items.Add(pres.From.Resource);
                        }
                        if (pres.MucUser.Item.Affiliation == agsXMPP.protocol.x.muc.Affiliation.member)
                        {
                            listBox3.Items.Add(pres.From.Resource);
                        }
                        if (pres.MucUser.Item.Affiliation == agsXMPP.protocol.x.muc.Affiliation.none)
                        {
                            listBox3.Items.Add(pres.From.Resource);
                        }

                        if (pres.Type == PresenceType.unavailable)
                        {
                            if (pres.MucUser.Item.Affiliation == agsXMPP.protocol.x.muc.Affiliation.owner)
                            {
                                listBox3.Items.Remove(pres.From.Resource);
                            }
                            if (pres.MucUser.Item.Affiliation == agsXMPP.protocol.x.muc.Affiliation.admin)
                            {
                                listBox3.Items.Remove(pres.From.Resource);
                            }
                            if (pres.MucUser.Item.Affiliation == agsXMPP.protocol.x.muc.Affiliation.member)
                            {
                                listBox3.Items.Remove(pres.From.Resource);
                            }
                            if (pres.MucUser.Item.Affiliation == agsXMPP.protocol.x.muc.Affiliation.none)
                            {
                                listBox3.Items.Remove(pres.From.Resource);
                            }
                        }
                    }
               
            }

            

        }

        
        //   private void XmppCon_OnWriteXml(object sender, string xml)
        //    {
        //          if (InvokeRequired)
        //        {
        //             BeginInvoke(new XmlHandler(XmppCon_OnWriteXml), new object[] { sender, xml });
        //             return;
        //        }
        //   }

        
        private void button4_Click(object sender, EventArgs e)
        {
            agsXMPP.protocol.client.Message msg = new agsXMPP.protocol.client.Message();
            msg.Type = MessageType.groupchat;
            msg.To = Roomjid;
            msg.Body = textBox5.Text;
            bcon.Send(msg);
            textBox5.Clear();
        }

       
        
        private void button6_Click(object sender, EventArgs e)
        { 
           manager.LeaveRoom(Roomjid, Username);
           listBox3.Items.Clear();
           pictureBox1.Hide();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

            

    }
}
