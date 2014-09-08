using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace VolnovNotificator
{
    class MessagesReceive
    {
        private static string domain = "http://prankota.com/";

        private static string GetHttpContent(string query)
        {
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                return webClient.DownloadString(domain + query);
            }
        }

        public static KeyValuePair<string, string> GetLastMessages()
        {
            return GetMessages("/not.txt");
        }

        private static KeyValuePair<string, string> GetMessages(string query)
        {
            string messagesContent = GetHttpContent(query);
            KeyValuePair<string, string> messageValuePair = new KeyValuePair<string, string>();

            string[] message = messagesContent.Split(';');
            if (message.Length == 2)
                messageValuePair = new KeyValuePair<string, string>(message[0], message[1]);
            else
            {
                if (message.Length == 1)
                    messageValuePair = new KeyValuePair<string, string>(message[0], null);
            }
            return messageValuePair;
        }
    }
}