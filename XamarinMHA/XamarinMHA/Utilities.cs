using AppointmentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinMHA
{
    public static class Utilities
    {
        //public static String LOCALHOST = "http://192.168.1.120:8080/";
        //public static String LOCALHOST = "http://10.0.3.2:8080/";
        public static String LOCALHOST = "https://keyfort.herokuapp.com/";

        public static void toggleSpinner (ActivityIndicator spinner) {
            spinner.IsVisible = !spinner.IsVisible;
            spinner.IsRunning = !spinner.IsRunning;
        }

        public static DateTime FindDateTimeBasedOnSlotNumber(DateTime date, int slotNumber)
        {
            DateTime completeDate;
            if (slotNumber % 2 == 0)
            {
                return completeDate = date.AddHours(slotNumber / 2);
            } else
            {
                return completeDate = date.AddHours(slotNumber / 2).AddMinutes(30);
            }
        }

        public static String FindTimeBaseOnSlotNumber(int slotNumber)
        {
            if (slotNumber % 2 == 0)
            {
                if (slotNumber/2 < 10)
                {
                    return "0" + (slotNumber / 2).ToString() + ":00";
                } else
                {
                    return (slotNumber / 2).ToString() + ":00";
                }
            }
            else
            {
                if (slotNumber / 2 < 10)
                {
                    return "0" + (slotNumber / 2) + ":30";
                }
                else
                {
                    return (slotNumber / 2) + ":30";
                }
            }
        }

        public static int FindSlotNumberBasedOnTime(String time)
        {
            int hour = Int16.Parse(time.Split(':')[0]);
            var minutes = Int16.Parse(time.Split(':')[1]);
            var slot = hour * 2;
            if (minutes == 30) slot++;
            return slot;
        }

        public static List<int> filterAvailableSlotsBasedOnDuration(List<int> allAvailableSlots, int duration)
        {
            List<int> filteredSlots = new List<int>();
            foreach (int aSlot in allAvailableSlots)
            {
                Boolean flag = true;
                for (int i = aSlot; i <= aSlot + duration; i++) {
                    if (!allAvailableSlots.Contains(i))
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    filteredSlots.Add(aSlot);
                }
            }
            return filteredSlots;
        }

        public static String createNotificationIdFromDateAndSlot(DateTime date, int slotNumber)
        {
            return date.Year.ToString() + date.Month.ToString() + date.Day.ToString() + slotNumber.ToString();
        }

        public static DateTime createDateTimeFromDateAndSlotNumber(DateTime date, int slotNumber)
        {
            String time = Utilities.FindTimeBaseOnSlotNumber(slotNumber);
            int hour = Int16.Parse(time.Split(':')[0]);
            var minutes = Int16.Parse(time.Split(':')[1]);
            return date.AddHours(hour).AddMinutes(minutes);
        }
    }

    //TODO: Suggest resources in session logging
    //TODO: Multiple pictures upload for user registration
    //TODO: Implement download resource button functionality
    //TODO: Add button send to email in resources details page
    //TODO: Populate SOS page
    //TODO: Search in favorite resources is broken
    //TODO: Send email when appointment is set that includes pdfs regarding cancelation policy etc.
    //TODO: Display description in detailed resources (both free and user)
    //TODO: Mentor and Admin profile is using UserProfilePage which is wrong. Create 2 almost identical pages to show profiles of mentors & admins
    //TODO: My appointments for mentor implementation
}
