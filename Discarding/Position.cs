using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Discarding
{
    internal class Position : IComparable<Position>, IEquatable<Position>
    {
        public string VendorCode { get; set; }
        public string Name { get; set; }
        public decimal? Amount { get; set; }
        public string Units { get; set; }
        public string Comments { get; set; }
        public string Date { get; set; }
        public string From { get; set; }
        private Position()
        {
        }
        public static Position Default(string vendorCode, string name, decimal? amount, string date, string from)
        {
            Position position = new Position();
            position.VendorCode = vendorCode;
            position.Name = name;
            position.Amount = amount;
            position.Date = date;
            position.From = from;
            return position;
        }
        public static Position WhithUnits(string vendorCode, string name, decimal? amount, string units, string date, string from)
        {
            Position position = new Position();
            position.VendorCode = vendorCode;
            position.Name = name;
            position.Amount = amount;
            position.Units = units;
            position.Date = date;
            position.From = from;
            return position;
        }
        public static Position WithComments(string vendorCode, string name, decimal? amount, string comments, string date, string from)
        {
            Position position = new Position();
            position.VendorCode = vendorCode;
            position.Name = name;
            position.Amount = amount;
            position.Comments = comments;
            position.Date = date;
            position.From = from;
            return position;
        }
        public static Position WithUnitsAndComments(string vendorCode, string name, decimal? amount, string units, string comments, string date, string from)
        {
            Position position = new Position();
            position.VendorCode = vendorCode;
            position.Name = name;
            position.Amount = amount;
            position.Units = units;
            position.Comments = comments;
            position.Date = date;
            position.From = from;
            return position;
        }
        public static Position Error(string name, string date, string from)
        {
            Position position = new Position();
            position.Name = name;
            position.Date = date;
            position.From = from;
            return position;
        }
        public override string ToString()
        {
            return $"Статья: {VendorCode}" + $"  Позиция: {Name}" + $"  Колличество: {Amount}" + $"  Ед. Измерения: {Units}" + $"  Комментарий: {Comments}" + $"  Дата: {Date}" + $"  От: {From}";
        }
        public int CompareTo(Position position)
        {
            if (ReferenceEquals(this, position)) return 0;
            if (VendorCode == null && position.VendorCode != null) return -1;
            if (VendorCode != null && position.VendorCode == null) return 1;
            if (ReferenceEquals(VendorCode, position.VendorCode) || VendorCode.CompareTo(position.VendorCode) == 0)
            {
                if (Name == null && position.Name != null) return -1;
                if (Name != null && position.Name == null) return 1;
                if (ReferenceEquals(Name, position.Name) || Name.CompareTo(position.Name) == 0)
                {
                    if (Units == null && position.Units != null) return -1;
                    if (Units != null && position.Units == null) return 1;
                    if (ReferenceEquals(Units, position.Units) || Units.CompareTo(position.Units) == 0) return 0;
                    return this.Units.CompareTo(position.Units);
                }
                else return this.Name.CompareTo(position.Name);
            }
            else return this.VendorCode.CompareTo(position.VendorCode);
        }

        public bool Equals(Position position)
        {
            if (this.VendorCode == position.VendorCode &&
                this.Name       == position.Name &&
                this.Amount     == position.Amount &&
                this.Units      == position.Units &&
                this.Comments   == position.Comments &&
                this.Date       == position.Date &&
                this.From       == position.From)
            {
                return true;
            }
            else return false;
        }
    }
}
