using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string data = File.ReadAllText("D:\\Study\\C#\\задания\\Discarding\\Discarding\\result.json");
            Message chat = JsonSerializer.Deserialize<Message>(data);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true,
                IgnoreNullValues = true,
            };
            string position1 = JsonSerializer.Serialize(chat, options);
            Console.WriteLine(position1);

            List<Position> positionsList = new List<Position>();
            string vendorCode = null;
            string name = null;
            double? amount = null;
            string units = null;
            string date = null;
            string from = null;
            string comment = null;

            Regex regexVendor = new Regex(@"^\s*(\w+\s*\S*\s*\S*\s*\S*\s*\S*)\:\s*$", RegexOptions.Multiline);
            Regex regexName = new Regex(@"^\s*(\w+\s*\S*\s*\S*\s*\S*)\s*\-", RegexOptions.Multiline);
            Regex regexAmount = new Regex(@"\-*\s*(\d+\.*\d*)", RegexOptions.Multiline);
            Regex regexUnits = new Regex(@"\d\s*([А-Яа-яЁё]+)", RegexOptions.Multiline);
            Regex regexComment = new Regex(@"\(([А-Яа-яЁё]+)\)", RegexOptions.Multiline);


            for (int i = 0; i < chat.Messages.Count; i++)
            {
                string[] message = chat.Messages[i].Text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int x = 0; x < message.Length; x++)
                {
                    Match matchVendor = regexVendor.Match(message[x]);
                    if (matchVendor.Success)
                    {
                        vendorCode = matchVendor.Groups[1].Value;
                        continue;
                    }
                    Match matchName = regexName.Match(message[x]);
                    if (matchName.Success)
                    {
                        name = matchName.Groups[1].Value;
                    }
                    Match matchAmount = regexAmount.Match(message[x]);
                    if (matchAmount.Success)
                    {
                        amount = Convert.ToDouble(matchAmount.Groups[1].Value.Replace(".", ","));
                    }
                    Match matchUnits = regexUnits.Match(message[x]);
                    if (matchUnits.Success)
                    {
                        units = matchUnits.Groups[1].Value;
                    }
                    Match matchComment = regexComment.Match(message[x]);
                    if (matchComment.Success)
                    {
                        comment = matchComment.Groups[1].Value;
                    }
                    if (name != null & amount != null & units == null & comment == null)
                    {
                        positionsList.Add(Position.Default(vendorCode, name, amount, chat.Messages[i].Date, chat.Messages[i].From));
                        name = null;
                        amount = null;
                    }
                    else if (name != null & amount != null & units != null & comment == null)
                    {
                        positionsList.Add(Position.WhithUnits(vendorCode, name, amount, units, chat.Messages[i].Date, chat.Messages[i].From));
                        name = null;
                        amount = null;
                        units = null;
                    }
                    else if (name != null & amount != null & units == null & comment != null)
                    {
                        positionsList.Add(Position.WithComments(vendorCode, name, amount, comment, chat.Messages[i].Date, chat.Messages[i].From));
                        name = null;
                        amount = null;
                        comment = null;
                    }
                    else if (name != null & amount != null & units != null & comment != null)
                    {
                        positionsList.Add(Position.WithUnitsAndComments(vendorCode, name, amount, units, comment, chat.Messages[i].Date, chat.Messages[i].From));
                        name = null;
                        amount = null;
                        units = null;
                        comment = null;
                    }
                    else
                    {
                        positionsList.Add(Position.Error(message[x], chat.Messages[i].Date, chat.Messages[i].From));
                        name = null;
                        amount = null;
                        units = null;
                        comment = null;
                    }
                }
            }
            foreach (Position position in positionsList)
            {
                Console.WriteLine(position);
            }
        }
    }
}
