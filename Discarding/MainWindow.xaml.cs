using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace Discarding
{
    public partial class MainWindow : Window
    {
        List<Position> positionsList = new List<Position>();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Json_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            string data = File.ReadAllText(openFileDialog.FileName);

            Message chat = JsonSerializer.Deserialize<Message>(data);

            string vendorCode = null;
            string name = null;
            decimal? amount = null;
            string units = null;
            string comment = null;

            Regex regexVendor = new Regex(@"^\s*(\w+\s*\S*\s*\S*\s*\S*\s*\S*)\:\s*$", RegexOptions.Multiline);
            Regex regexName = new Regex(@"^\s*(\w+\s*\S*\s*\S*\s*\S*)\s*\-", RegexOptions.Multiline);
            Regex regexAmount = new Regex(@"\-*\s*(\d+\.*\,*\d*)", RegexOptions.Multiline);
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
                        vendorCode = (matchVendor.Groups[1].Value).Trim().ToLower();
                        continue;
                    }
                    Match matchName = regexName.Match(message[x]);
                    if (matchName.Success)
                    {
                        name = matchName.Groups[1].Value.Trim().ToLower();
                    }
                    Match matchAmount = regexAmount.Match(message[x]);
                    if (matchAmount.Success)
                    {
                        amount = Convert.ToDecimal(matchAmount.Groups[1].Value.Replace(".", ","));
                        if (amount <= 0) amount = null;
                    }
                    Match matchUnits = regexUnits.Match(message[x]);
                    if (matchUnits.Success)
                    {
                        units = matchUnits.Groups[1].Value.ToLower();
                    }
                    Match matchComment = regexComment.Match(message[x]);
                    if (matchComment.Success)
                    {
                        comment = matchComment.Groups[1].Value;
                    }
                    if (name != null & amount != null & units == null & comment == null)
                    {
                        positionsList.Add(Position.Default(vendorCode, name, amount, (DateTime.Parse(chat.Messages[i].Date)).ToString("d"), chat.Messages[i].From));
                        name = null;
                        amount = null;
                    }
                    else if (name != null & amount != null & units != null & comment == null)
                    {
                        positionsList.Add(Position.WhithUnits(vendorCode, name, amount, units, (DateTime.Parse(chat.Messages[i].Date)).ToString("d"), chat.Messages[i].From));
                        name = null;
                        amount = null;
                        units = null;
                    }
                    else if (name != null & amount != null & units == null & comment != null)
                    {
                        positionsList.Add(Position.WithComments(vendorCode, name, amount, comment, (DateTime.Parse(chat.Messages[i].Date)).ToString("d"), chat.Messages[i].From));
                        name = null;
                        amount = null;
                        comment = null;
                    }
                    else if (name != null & amount != null & units != null & comment != null)
                    {
                        positionsList.Add(Position.WithUnitsAndComments(vendorCode, name, amount, units, comment, (DateTime.Parse(chat.Messages[i].Date)).ToString("d"), chat.Messages[i].From));
                        name = null;
                        amount = null;
                        units = null;
                        comment = null;
                    }
                    else
                    {
                        positionsList.Add(Position.Error(message[x], (DateTime.Parse(chat.Messages[i].Date)).ToString("d"), chat.Messages[i].From));
                        name = null;
                        amount = null;
                        units = null;
                        comment = null;
                    }
                }
            }
            positionsList.Sort();
            content.ItemsSource = positionsList;
        }
        private void First_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < positionsList.Count - 1; i++)
            {
                if (positionsList[i].VendorCode != null)
                {
                    if (positionsList[i].VendorCode == positionsList[i + 1].VendorCode &&
                        positionsList[i].Name == positionsList[i + 1].Name &&
                        positionsList[i].Units == positionsList[i + 1].Units &&
                        positionsList[i].Comments == null && positionsList[i + 1].Comments == null)
                    {
                        positionsList[i].Amount += positionsList[i + 1].Amount;
                        positionsList[i].From = null;
                        positionsList[i].Date = null;
                        positionsList.RemoveAt(i + 1);
                        i--;
                    }
                }
            }
            content.ItemsSource = positionsList;
            content.Items.Refresh();
        }
        private void Merge_Click(object sender, RoutedEventArgs e)
        {
            List <Position> miniList = content.SelectedItems.Cast<Position>().ToList();
            miniList.Sort();
            int index = positionsList.FindIndex(x => x.Equals(miniList[0]));
            for (int i = 0; i < miniList.Count; i++)
            {
                positionsList.Remove(miniList[i]);
            }
            for (int i = 1; i < miniList.Count; i++)
            {
                miniList[0].Amount += miniList[i].Amount;
            }
            miniList[0].Comments = null;
            miniList[0].Date = null;
            miniList[0].From = null;
            positionsList.Insert(index, miniList[0]);
            content.ItemsSource = positionsList;
            content.Items.Refresh();
        }
    }
}
