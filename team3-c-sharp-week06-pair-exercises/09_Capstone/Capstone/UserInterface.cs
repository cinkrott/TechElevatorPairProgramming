using Capstone.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone
{
    public class UserInterface
    {
        //ALL Console.ReadLine and WriteLine in this class
        //NONE in any other class

        private string connectionString;
        private VenueDAO venueDAO;
        private SpaceDAO spaceDAO;
        private ReservationDAO reservationDAO;
        private CategoryDAO categoryDAO;

        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            venueDAO = new VenueDAO(connectionString);
            spaceDAO = new SpaceDAO(connectionString);
            reservationDAO = new ReservationDAO(connectionString);
            categoryDAO = new CategoryDAO(connectionString);
        }

       

        public void Run()
        {
            bool done = false;
            while (!done)
            {
                string result = DisplayAdvancedSearchMainMenu();

                switch (result.ToLower())
                {
                    case "1":
                        RunVenueMenu();
                        break;

                    case "2":
                        RunAdvancedSearch();
                        break;
                    case "q":
                        done = true;
                      
                        break;

                    default:
                        Console.WriteLine("Please make a valid selection.");
                        break;
                }
            }
            return;
        }

        private string DisplayAdvancedSearchMainMenu()
        {
            Console.WriteLine("Welcome to Excelsior Venues!");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1) List Venues");
            Console.WriteLine("2) Search for a Space");
            Console.WriteLine("Q) Quit");

            return Console.ReadLine();

        }

        private void RunAdvancedSearch()
        {
            bool done = false;

            while (!done)
            {
                try
                {
                    List<Venue> venues = venueDAO.GetAllVenues();
                    List<Space> allSpaceResults = new List<Space>();

                    Console.WriteLine();
                    Console.Write("When do you need the space? ");
                    DateTime startDate = DateTime.Parse(Console.ReadLine());

                    Console.Write("How many days will you need the space? ");
                    int numberOfDays = int.Parse(Console.ReadLine());

                    Console.Write("How many people will be in attendance? ");
                    int numberOfPeople = int.Parse(Console.ReadLine());

                    Console.Write("Does the space require accessibility accommodations (Y/N)? ");
                    string accessibilityRequirementInput = Console.ReadLine();
                    bool accessibilityRequirement;
                    if (accessibilityRequirementInput.ToUpper() == "Y")
                    {
                        accessibilityRequirement = true;
                    }
                    else if (accessibilityRequirementInput.ToUpper() == "N")
                    {
                        accessibilityRequirement = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter Y or N.");
                        continue;
                    }

                    Console.Write("What is your daily budget? ");
                    decimal dailyBudget = decimal.Parse(Console.ReadLine());

                    Console.WriteLine("Which of the categories would you like to include? ");

                    List<Category> allCategories = categoryDAO.GetAllCategories();
                    for (int i = 0; i < allCategories.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}) {allCategories[i]}");
                    }
                    Console.WriteLine("N) None");
                    string selectedCategory = Console.ReadLine();
                    if (selectedCategory.ToUpper() == "N")
                    {
                        Console.WriteLine("The following venues and spaces are available based on your needs:");

                        for (int i = 0; i < venues.Count; i++)
                        {
                            List<Space> matchingSpaces = spaceDAO.SearchSpaceAvailabilityWithoutCategory(venues[i], startDate, numberOfDays,
                                                         numberOfPeople, accessibilityRequirement, dailyBudget);
                            if (matchingSpaces.Count > 0)
                            {
                                allSpaceResults.AddRange(matchingSpaces);
                                Console.WriteLine(venues[i].Name);
                                Console.WriteLine();
                                Console.WriteLine("Space ID#   Name                            Daily Rate   Max Occup.   Accessible?    Total Cost");

                                for (int j = 0; j < matchingSpaces.Count; j++)
                                {
                                    Console.WriteLine($"{matchingSpaces[j].Id.ToString().PadRight(11)} {(matchingSpaces[j].Name).PadRight(30)} " +
                                        $"{matchingSpaces[j].DailyRate.ToString("C").PadLeft(11)}   {matchingSpaces[j].MaxOccupancy.ToString().PadRight(12)} " +
                                        $"{GetIsAccessibleString(matchingSpaces[j].IsAccessible).PadRight(13)} " +
                                        $"{(numberOfDays * matchingSpaces[j].DailyRate).ToString("C").PadLeft(11)}");
                                }
                                Console.WriteLine();
                            }
                        }

                    }
                    else
                    {
                        int selectedCategoryNumber = int.Parse(selectedCategory);
                        int selectedCategoryId = allCategories[selectedCategoryNumber - 1].Id;
                        Console.WriteLine("The following venues and spaces are available based on your needs:");

                        for (int i = 0; i < venues.Count; i++)
                        {
                            List<Space> matchingSpaces = spaceDAO.SearchSpaceAvailabilityWithCategory(venues[i], startDate, numberOfDays,
                                                         numberOfPeople, accessibilityRequirement, dailyBudget, selectedCategoryId);
                            if (matchingSpaces.Count > 0)
                            {
                                allSpaceResults.AddRange(matchingSpaces);
                                Console.WriteLine(venues[i].Name);
                                Console.WriteLine();
                                Console.WriteLine("Space ID#   Name                            Daily Rate   Max Occup.   Accessible?    Total Cost");

                                for (int j = 0; j < matchingSpaces.Count; j++)
                                {
                                    Console.WriteLine($"{matchingSpaces[j].Id.ToString().PadRight(11)} {(matchingSpaces[j].Name).PadRight(30)} " +
                                        $"{matchingSpaces[j].DailyRate.ToString("C").PadLeft(11)}   {matchingSpaces[j].MaxOccupancy.ToString().PadRight(12)} " +
                                        $"{GetIsAccessibleString(matchingSpaces[j].IsAccessible).PadRight(13)} " +
                                        $"{(numberOfDays * matchingSpaces[j].DailyRate).ToString("C").PadLeft(11)}");
                                }
                                Console.WriteLine();
                            }
                        }
                    }


                    if (allSpaceResults.Count == 0)
                    {
                        Console.Write("I'm sorry, no results found matching the criteria. Would you like to try a different search? (Y/N) ");
                        string input = Console.ReadLine();
                        if (input.ToUpper() == "Y")
                        {
                            continue;
                        }
                        else if (input.ToUpper() == "N")
                        {
                            done = true;
                            break;
                        }
                    }

                    bool doneEnteringSpaceId = false;
                    while (!doneEnteringSpaceId)
                    {
                        Console.WriteLine("");
                        Console.Write("Which space would you like to reserve (enter 0 to cancel)? ");
                        int spaceToReserve = int.Parse(Console.ReadLine());

                        if (spaceToReserve == 0)
                        {
                            doneEnteringSpaceId = true;
                            done = true;
                            break;
                        }
                        else
                        {
                            bool invalidSpaceIdEntered = true;
                            foreach (Space space in allSpaceResults)
                            {
                                if (space.Id == spaceToReserve)
                                {
                                    Console.Write("Who is this reservation for? ");
                                    string reservationName = Console.ReadLine();
                                    Console.WriteLine();

                                    //check if numberOfDays is accurate
                                    Reservation reservation = reservationDAO.ReserveSpace(spaceToReserve, numberOfPeople, startDate, (startDate.AddDays(numberOfDays - 1)), reservationName);

                                    Console.WriteLine();
                                    Console.WriteLine("Thanks for submitting your reservation! The details for your event are listed below: ");
                                    Console.WriteLine();
                                    Console.WriteLine($"Confirmation #: {reservation.ReservationId}");
                                    Console.WriteLine($"         Venue: {venueDAO.GetVenueNameFromId(space.VenueId)}");
                                    Console.WriteLine($"         Space: {spaceDAO.GetSpaceNameFromId(spaceToReserve)}");
                                    Console.WriteLine($"  Reserved For: {reservation.ReservedFor}");
                                    Console.WriteLine($"     Attendees: {reservation.NumberOfAttendees}");
                                    Console.WriteLine($"  Arrival Date: {reservation.StartDate.ToShortDateString()}");
                                    Console.WriteLine($"   Depart Date: {reservation.EndDate.ToShortDateString()}");
                                    Console.WriteLine($"    Total Cost: {(spaceDAO.GetSpaceDailyRateFromId(spaceToReserve) * numberOfDays).ToString("C")}");
                                    Console.WriteLine();

                                    invalidSpaceIdEntered = false;
                                    doneEnteringSpaceId = true;
                                    done = true;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            if (invalidSpaceIdEntered)
                            {
                                Console.WriteLine("Please enter 0 or an above listed space ID.");
                            }
                        }
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException formatEx)
                {
                    Console.WriteLine(formatEx.Message);
                }
            }
        }

        private void RunVenueMenu()
        {
            List<Venue> venues = venueDAO.GetAllVenues();

            bool done = false;

            while (!done)
            {
                for (int i = 0; i < venues.Count; i++)
                {
                    Console.WriteLine($"{i + 1}) {venues[i].Name}");
                }
                Console.WriteLine("R) Return to Previous Screen");
                string selection = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    if (selection.ToUpper() == "R")
                    {
                        done = true;
                        break;
                    }
                    else
                    {
                        int venuesIndex = Int32.Parse(selection) - 1;
                        if (venuesIndex >= 0 && venuesIndex < venues.Count)
                        {
                            ViewVenue(venues[venuesIndex]);
                        }
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Please enter a valid menu option.");
                }
            }
        }

        private void ViewVenue(Venue venue)
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine(venue.Name);
                Console.WriteLine($"Location: {venueDAO.GetCity(venue.CityId)}, {venueDAO.GetState(venue.CityId)}");
                Console.WriteLine("Categories: ");
                List<string> categories = venueDAO.GetCategories(venue);

                foreach (String category in categories)
                {
                    Console.WriteLine($"            {category}");
                }
                Console.WriteLine();

                Console.WriteLine(venue.Description);
                Console.WriteLine();

                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("1) View Spaces");
                Console.WriteLine("2) Reserve a Space");
                Console.WriteLine("R) Return to Previous Screen");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input.ToUpper())
                {
                    case "1":
                        RunSpacesMenu(venue);
                        break;
                    case "2":
                        RunReservationMenu(venue);
                        break;
                    case "R":
                        Console.WriteLine();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid menu option.");
                        break;
                }
            }
        }

        private void RunSpacesMenu(Venue venue)
        {
            List<Space> spaces = spaceDAO.GetVenueSpaces(venue);

            bool done = false;
            while (!done)
            {

                Console.WriteLine($"{venue.Name} Spaces");
                Console.WriteLine();
                Console.WriteLine("     Name".PadRight(40) + "Open   Close   Daily Rate   Max. Occupancy");

                for (int i = 0; i < spaces.Count; i++)
                {
                    Console.WriteLine($"#{(i + 1).ToString().PadRight(3)} {(spaces[i].Name).PadRight(34)} {GetMonthName(spaces[i].OpenFrom).PadRight(6)} {GetMonthName(spaces[i].OpenTo).PadRight(7)} {spaces[i].DailyRate.ToString("C").PadLeft(10)}   {spaces[i].MaxOccupancy}");
                }

                Console.WriteLine();
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("1) Reserve a Space");
                Console.WriteLine("R) Return to Previous Screen");
                string input = Console.ReadLine();


                switch (input.ToUpper())
                {
                    case "1":
                        RunReservationMenu(venue);
                        break;
                    case "R":
                        Console.WriteLine();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid menu option.");
                        break;
                }

            }
        }

        private void RunReservationMenu(Venue venue)
        {
            List<Venue> result = new List<Venue>();
            bool done = false;

            while (!done)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("When do you need the space? ");
                    DateTime startDate = DateTime.Parse(Console.ReadLine());

                    Console.Write("How many days will you need the space? ");
                    int numberOfDays = int.Parse(Console.ReadLine());

                    Console.Write("How many people will be in attendance? ");
                    int numberOfPeople = int.Parse(Console.ReadLine());

                    List<Space> matchingSpaces = spaceDAO.SearchSpaceAvailability(venue, startDate, numberOfDays, numberOfPeople);

                    if (matchingSpaces.Count == 0)
                    {
                        Console.Write("I'm sorry, no results found matching the criteria. Would you like to try a different search? (Y/N) ");
                        string input = Console.ReadLine();
                        if (input.ToUpper() == "Y")
                        {
                            continue;
                        }
                        else if (input.ToUpper() == "N")
                        {
                            done = true;
                            break;
                        }
                    }

                    Console.WriteLine();
                    Console.WriteLine("The Following spaces are available based on your needs:");
                    Console.WriteLine();
                    Console.WriteLine("Space ID#   Name                            Daily Rate   Max Occup.   Accessible?    Total Cost");

                    for (int i = 0; i < matchingSpaces.Count; i++)
                    {
                        Console.WriteLine($"{matchingSpaces[i].Id.ToString().PadRight(11)} {(matchingSpaces[i].Name).PadRight(30)} " +
                            $"{matchingSpaces[i].DailyRate.ToString("C").PadLeft(11)}   {matchingSpaces[i].MaxOccupancy.ToString().PadRight(12)} " +
                            $"{GetIsAccessibleString(matchingSpaces[i].IsAccessible).PadRight(13)} " +
                            $"{(numberOfDays * matchingSpaces[i].DailyRate).ToString("C").PadLeft(11)}");
                    }

                    bool doneEnteringSpaceId = false;

                    try
                    {
                        while (!doneEnteringSpaceId)
                        {
                            Console.WriteLine("");
                            Console.Write("Which space would you like to reserve (enter 0 to cancel)? ");
                            int spaceToReserve = int.Parse(Console.ReadLine());

                            if (spaceToReserve == 0)
                            {
                                doneEnteringSpaceId = true;
                                done = true;
                                break;
                            }
                            else
                            {
                                bool invalidSpaceIdEntered = true;
                                foreach (Space space in matchingSpaces)
                                {
                                    if (space.Id == spaceToReserve)
                                    {
                                        Console.Write("Who is this reservation for? ");
                                        string reservationName = Console.ReadLine();
                                        Console.WriteLine();

                                      
                                        Reservation reservation = reservationDAO.ReserveSpace(spaceToReserve, numberOfPeople, startDate, (startDate.AddDays(numberOfDays - 1)), reservationName);

                                        Console.WriteLine();
                                        Console.WriteLine("Thanks for submitting your reservation! The details for your event are listed below: ");
                                        Console.WriteLine();
                                        Console.WriteLine($"Confirmation #: {reservation.ReservationId}");
                                        Console.WriteLine($"         Venue: {venue.Name}");
                                        Console.WriteLine($"         Space: {spaceDAO.GetSpaceNameFromId(spaceToReserve)}");
                                        Console.WriteLine($"  Reserved For: {reservation.ReservedFor}");
                                        Console.WriteLine($"     Attendees: {reservation.NumberOfAttendees}");
                                        Console.WriteLine($"  Arrival Date: {reservation.StartDate.ToShortDateString()}");
                                        Console.WriteLine($"   Depart Date: {reservation.EndDate.ToShortDateString()}");
                                        Console.WriteLine($"    Total Cost: {(spaceDAO.GetSpaceDailyRateFromId(spaceToReserve) * numberOfDays).ToString("C")}");
                                        Console.WriteLine();

                                        invalidSpaceIdEntered = false;
                                        doneEnteringSpaceId = true;
                                        done = true;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                if (invalidSpaceIdEntered)
                                {
                                    Console.WriteLine("Please enter 0 or an above listed space ID.");
                                }
                            }
                        }
                    }
                    catch (FormatException ex2)
                    {
                        Console.WriteLine(ex2.Message);
                        Console.WriteLine("Please enter a valid option.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please enter a valid option.");
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine(sqlEx.Message);
                }
            }
        }


        // Helper methods
        private string GetMonthName(int monthNumber)
        {
            string monthName = "";
            switch (monthNumber)
            {
                case 1:
                    monthName = "Jan.";
                    break;
                case 2:
                    monthName = "Feb.";
                    break;
                case 3:
                    monthName = "Mar.";
                    break;
                case 4:
                    monthName = "Apr.";
                    break;
                case 5:
                    monthName = "May.";
                    break;
                case 6:
                    monthName = "Jun.";
                    break;
                case 7:
                    monthName = "Jul.";
                    break;
                case 8:
                    monthName = "Aug.";
                    break;
                case 9:
                    monthName = "Sep.";
                    break;
                case 10:
                    monthName = "Oct.";
                    break;
                case 11:
                    monthName = "Nov.";
                    break;
                case 12:
                    monthName = "Dec.";
                    break;
                default:
                    break;
            }

            return monthName;
        }

        private string GetIsAccessibleString(bool isAccessible)
        {
            string result = "";

            if (isAccessible)
            {
                result = "Yes";
            }
            else
            {
                result = "No";
            }

            return result;
        }
    }
}
