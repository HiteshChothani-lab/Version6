using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using UserManagement.Common.Constants;

namespace UserManagement.Entity
{
    public class StoreUsersResponseEntity : EntityBase
    {
        public List<StoreUserEntity> Data { get; set; }
        public string Status { get; set; }
        public string Messagee { get; set; }
    }
    public class ArchieveStoreUsersResponseEntity : EntityBase
    {
        public long ArchieveSize { get; set; }
        public List<StoreUserEntity> Data { get; set; }
        public string Status { get; set; }
        public string Messagee { get; set; }
    }

    public class StoreUserEntity : INotifyPropertyChanged
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string SuperMasterId { get; set; }
        public string MasterStoreId { get; set; }
        public string StoreId { get; set; }
        public string OrderId { get; set; }
        public string Btn1 { get; set; }
        public string Btn2 { get; set; }
        public string Btn3 { get; set; }
        public string Btn4 { get; set; }
        public string Btn5 { get; set; }
        public string BtnAB { get; set; }
        public string Note { get; set; }
        public string NoteColor { get; set; }
        public string Tag { get; set; }
        public string OrphanStatus { get; set; }
        public string RecentStatus { get; set; }
        public string DeliverOrderStatus { get; set; }
        public string IdrStatus { get; set; }
        public string AccountBlockStatus { get; set; }
        public string BadExpDesc { get; set; }
        public string Age { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CountryCode { get; set; }
        public string Mobile { get; set; }
        public string Status { get; set; }
        public string HomePhone { get; set; }
        public string PostalCode { get; set; }
        public string RegisterType { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Gender { get; set; }
        public string RoomNumber { get; set; }
        public string DOB { get; set; }
        public string TimeDifference { get; set; }
        public string ExpressTime { get; set; }
        public string RegType { get; set; }

        public bool IsFlagSet
        {
            get { return RecentStatus == "1" ? true : false; }
        }

        public bool IsZipCode
        {
            get { return string.IsNullOrWhiteSpace(PostalCode) ? false : Regex.IsMatch(PostalCode, "^[0-9]{5}$"); }
        }

        public string Column1TextDisplay
        {
            get => $"{Btn1} & {Btn2}".Trim().Trim('&');
        }

        public string Column1AgeDisplayImage
        {
            get
            {
                string path = string.Empty;

                if (int.TryParse(Age, out int age))
                {
                    if (age <= 16)
                    {
                        path = "/UserManagement.UI;component/Assets/icon_kid.png";
                    }
                    else if (age <= 64)
                    {
                        path = "/UserManagement.UI;component/Assets/icon_adult.png";
                    }
                    else
                    {
                        path = "/UserManagement.UI;component/Assets/icon_old.png";
                    }
                }

                return path;
            }
        }

        public string Column2FullNameDisplay
        {
            get
            {
                var str = $"{Firstname} {Lastname}".Trim(' ');
                return str.Length > 20 ? str.Insert(20, Environment.NewLine) : str;
            }
        }

        public string Column2IncompleteDisplay
        {
            get => string.IsNullOrEmpty(HomePhone) && string.IsNullOrEmpty(Mobile) ? "Incomplete" : string.Empty;
        }

        public string Column2IDRDisplayImage
        {
            get => IdrStatus == "1" && string.IsNullOrWhiteSpace(Mobile) ? "/UserManagement.UI;component/Assets/icon_check.png" : string.Empty;
        }

        public string Column2NewOrIdrDisplay
        {
            get => IdrStatus == "0" && string.IsNullOrWhiteSpace(Mobile) ? RegisterType == "first" ? "NEW" : "IDR" : string.Empty;
        }

        public string Column3PostalCode
        {
            get
            {
                if (!string.IsNullOrEmpty(Mobile))
                    return Mobile;
                else if (!string.IsNullOrEmpty(PostalCode))
                {
                    return IsZipCode ? $"Z {PostalCode}" : PostalCode;
                }
                else
                    return "Override";
            }
        }

        private string _column3FontColor = ColorNames.DarkerBlue;
        public string Column3FontColor
        {
            get
            {
                if (!string.IsNullOrEmpty(Mobile))
                {
                    _column3FontColor = IsFlagSet ? ColorNames.White : ColorNames.NavyBlue;
                }
                else if (!string.IsNullOrEmpty(PostalCode))
                {
                    _column3FontColor = IsFlagSet ? ColorNames.White : ColorNames.DarkerBlue;
                }
                else
                {
                    _column3FontColor = ColorNames.Red;
                }
                return _column3FontColor;
            }
            set
            {
                _column3FontColor = value;
                OnPropertyRaised("Column3FontColor");
            }
        }

        public string Column2RowColor { get; set; } = ColorNames.Green;

        public string Column3RowColor
        {
            get { return IsFlagSet ? ColorNames.Purple : ColorNames.White; }
        }

        public bool MobileNumberVisibility
        {
            get => !string.IsNullOrWhiteSpace(Mobile);
        }

        public bool HomePhoneNumberVisibility
        {
            get => !MobileNumberVisibility;
        }

        /*
         * [early - Yellow] [ready - Blue] [late - Red]
         */
        public string TimeDifferenceColor
        {
            get
            {
                if (RegType.Equals("Express"))
                {
                    if ("late".Equals(TimeDifference))
                    {
                        return ColorNames.Red;
                    }
                    else if ("ready".Equals(TimeDifference))
                    {
                        return ColorNames.Blue;
                    }
                }
                return ColorNames.Yellow;
            }
        }

        public bool IsYellowNeedleVisible
        {
            get => Btn3.Contains("No deals ") || Btn3.Contains("Terrible Service");
        }

        public bool IsGreenNeedleVisible
        {
            get => Btn3.Contains("No deals ") || Btn3.Contains("No Deals") || Btn3.Contains("Very Terrible");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
    }
}
