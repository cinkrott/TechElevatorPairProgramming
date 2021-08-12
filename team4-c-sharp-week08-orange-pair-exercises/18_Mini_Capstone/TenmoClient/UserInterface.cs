using System;
using System.Collections.Generic;
using TenmoClient.APIClients;
using TenmoClient.Data;

namespace TenmoClient
{
    public class UserInterface
    {
        private readonly ConsoleService consoleService = new ConsoleService();
        private readonly AuthService authService = new AuthService();
        private readonly AccountService accountService = new AccountService();
        private readonly UserServiceAPI userServiceAPI = new UserServiceAPI();
        private readonly TransferService transfer = new TransferService();



        private bool shouldExit = false;

        public void Start()
        {
            while (!shouldExit)
            {
                while (!authService.IsLoggedIn)
                {
                    ShowLogInMenu();
                }

                // If we got here, then the user is logged in. Go ahead and show the main menu
                ShowMainMenu();
            }
        }

        private void ShowLogInMenu()
        {
            Console.WriteLine("Welcome to TEnmo!");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: Register");
            Console.Write("Please choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int loginRegister))
            {
                Console.WriteLine("Invalid input. Please enter only a number.");
            }
            else if (loginRegister == 1)
            {
                HandleUserLogin();
            }
            else if (loginRegister == 2)
            {
                HandleUserRegister();
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }

        private void ShowMainMenu()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests");
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else
                {
                    switch (menuSelection)
                    {
                        case 1:                            
                            UserBalance(); 
                            break;
                        case 2:
                            ViewTransfers();
                            break;
                        case 3:
                            Console.WriteLine("NOT IMPLEMENTED!"); // TODO: Implement me
                            break;
                        case 4:
                            TransferMenu();                           
                            break;
                        case 5:
                            // // TODO: Implement me
                            break;
                        case 6:
                            Console.WriteLine();
                            UserService.SetLogin(new API_User()); //wipe out previous login info
                            return;
                        default:
                            Console.WriteLine("Goodbye!");
                            shouldExit = true;
                            return;
                    }
                }
            }
        }

        private void HandleUserRegister()
        {
            bool isRegistered = false;

            while (!isRegistered) //will keep looping until user is registered
            {
                LoginUser registerUser = consoleService.PromptForLogin();
                isRegistered = authService.Register(registerUser);
            }

            Console.WriteLine("");
            Console.WriteLine("Registration successful. You can now log in.");
        }

        private void HandleUserLogin()
        {
            while (!UserService.IsLoggedIn) //will keep looping until user is logged in
            {
                LoginUser loginUser = consoleService.PromptForLogin();
                API_User user = authService.Login(loginUser);
                if (user != null)
                {
                    UserService.SetLogin(user);
                }
            }
        }

        public int userId = UserService.UserId;

        private void UserBalance()
        {
            decimal balance = accountService.GetBalance();
            Console.WriteLine("Your current account balance is: $" + balance);
        }

        private void TransferMenu()
        {
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Users");
            Console.WriteLine("ID        Name");
            Console.WriteLine("-------------------------------------------------");

            GetUsers();

            Console.WriteLine("----------------------");

            TransferFunds();

        }

        private void GetUsers()
        {
            List<API_User> users = userServiceAPI.GetUsers();

            foreach (API_User user in users)
            {
                if (user.UserId != UserService.UserId)
                {
                    Console.WriteLine(user.UserId + "       " + user.Username);
                }
            }

        }

       
        private void TransferFunds()
        {

            //TODO try/catch for format
            DataRequest dataRequest = new DataRequest();
            Console.Write("Enter ID of user you are sending to(0 to cancel): ");
            dataRequest.UserId = int.Parse(Console.ReadLine());
            Console.Write("Enter amount: ");
            dataRequest.AmountToTransfer = decimal.Parse(Console.ReadLine());
            decimal balance = accountService.GetBalance();

            if (balance >= dataRequest.AmountToTransfer)
            {
                bool result = accountService.Transfer(dataRequest);
                if (result)
                {
                    Console.WriteLine("Successful");
                }
                else
                {
                    Console.WriteLine("Not able to transfer.");
                }
            }
            else
            {
                Console.WriteLine("Insufficient Funds.");
            }


        }

        // transfer ID  usre input validation
        private bool TransferIdValidation(int transferId, List<Transfer> transfers)
        {
            bool result = false;
            try
            {
                foreach (Transfer transfer in transfers)
                {
                    if (transfer.TransferId == transferId)
                    {
                        result = true;
                    }
                }
            }
            catch (FormatException ex)
            {
                result = false;
            }
            return result;
        }

        private void ViewTransfers()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Transefers");
            Console.WriteLine("ID         From/To             Amount");
            Console.WriteLine("----------------------------------");
            List<Transfer> transferFrom = transfer.GetTransferFrom();
            List<Transfer> transferTo = transfer.GetTransferTo();

            foreach (Transfer transfer in transferFrom)
            {
                if (transfer.UserId != UserService.UserId)
                {
                    Console.WriteLine(transfer.TransferId + "        From:" + transfer.Name.PadRight(8) + "      " + transfer.Amount);

                }

            }
            foreach (Transfer transfer in transferTo)
            {
                if (transfer.UserId != UserService.UserId)
                {
                    Console.WriteLine(transfer.TransferId + "          To:" + transfer.Name.PadRight(8) + "      " + transfer.Amount);
                }

            }
            Console.WriteLine("-------------------");
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to view details (0 to cancel): ");
            int transferId = int.Parse(Console.ReadLine());
            bool transferIdValidation = TransferIdValidation(transferId, transferFrom);


            if (transferId == 0)
            {
                ShowMainMenu();
            }
            
            else if (transferIdValidation)
            {

                foreach (Transfer transfer in transferFrom)
                {
                    if (transfer.TransferId == transferId && transfer.Name != UserService.UserName)
                    {
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine("Transfer Details");
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine($"Id: {transfer.TransferId}");
                        Console.WriteLine($"From: {transfer.Name}");
                        Console.WriteLine($"To: {UserService.UserName}");
                        Console.WriteLine($"Type: {transfer.TransferType}");
                        Console.WriteLine($"Status: {transfer.TransferStatus}");
                        Console.WriteLine($"Amount: ${transfer.Amount}");
                    }
                }

                foreach (Transfer transfer in transferTo)
                {

                    if (transfer.TransferId == transferId && transfer.Name != UserService.UserName)
                    {
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine("Transfer Details");
                        Console.WriteLine("---------------------------------------------");
                        Console.WriteLine($"Id: {transfer.TransferId}");
                        Console.WriteLine($"From: {UserService.UserName}");
                        Console.WriteLine($"To: {transfer.Name}");
                        Console.WriteLine($"Type: {transfer.TransferType}");
                        Console.WriteLine($"Status: {transfer.TransferStatus}");
                        Console.WriteLine($"Amount: ${transfer.Amount}");

                    }

                }

            }
            else
            {
                Console.WriteLine("Invalid Transfer Id.");
            }
        }
    }
}
