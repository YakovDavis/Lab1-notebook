using System;
using System.Collections.Generic;
using System.Text;

namespace Notebook
{
    public static class CommandInterpreter
    {
        public static int RunCommand(string command, List<Note> notebook)
        {
            string[] comm = command.Split(' ');
            string[] args = null;
            int exitFlag = 0;
            if (comm.Length > 1)
            {
                args = new string[comm.Length - 1];
                Array.Copy(comm, 1, args, 0, comm.Length - 1);
            }
            switch (comm[0])
            {
                case "new":
                    NewNote(notebook, args);
                    break;
                case "edit":
                    EditNote(notebook, args);
                    break;
                case "print":
                    PrintNote(notebook, args);
                    break;
                case "printlist":
                    PrintNoteList(notebook, args);
                    break;
                case "delete":
                    DeleteNote(notebook, args);
                    break;
                case "help":
                    PrintHelp(args);
                    break;
                case "exit":
                    exitFlag = 1;
                    break;
                default:
                    Console.WriteLine("Unknown command. Type \"help\" for list of commands.");
                    break;
            }
            return exitFlag;
        }

        private static void NewNote(List<Note> notebook, string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("Usage \"new <first-name> <last-name> [patronymic] <phone-number> <country> [date-of-birth, organization, position, other-info]\"");
                return;
            }
            if ((args.Length < 3) || (args.Length > 9))
            {
                Console.WriteLine("Usage \"new <first-name> <last-name> [patronymic] <phone-number> <country> [date-of-birth, organization, position, other-info]\"");
                return;
            }

            Note note;
            int additArgsStart;

            if (IsStringANumber(args[2]))
            {
                note = new Note(args[0], args[1], args[2], args[3]);
                additArgsStart = 4;
            }
            else if (IsStringANumber(args[3]))
            {
                note = new Note(args[0], args[1], args[3], args[4]);
                note.PatronymicName = args[2];
                additArgsStart = 5;
            }
            else
            {
                Console.WriteLine("Phone number must only contain numbers.");
                return;
            }
            if (args.Length > additArgsStart)
            {
                note.DateOfBirth = args[additArgsStart];
                if (args.Length > additArgsStart + 1)
                {
                    note.Organization = args[additArgsStart + 1];
                    if (args.Length > additArgsStart + 2)
                    {
                        note.Position = args[additArgsStart + 2];
                        if (args.Length > additArgsStart + 3)
                            note.OtherNotes = args[additArgsStart + 3];
                    }
                }
            }
            notebook.Add(note);
            Console.WriteLine($"Successfully created a new note of {note} with id {note.Id}");
        }

        private static void EditNote(List<Note> notebook, string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("Usage \"edit <id> <field: 1 - first-name, 2 - last-name, 3 - patronymic, 4 - phone-number, 5 - country, 6 - date-of-birth, 7 - organization, 8 - position, 9 - other-info> <new-value> - edits a note\"");
                return;
            }
            if (args.Length != 3)
            {
                Console.WriteLine("Usage \"edit <id> <field: 1 - first-name, 2 - last-name, 3 - patronymic, 4 - phone-number, 5 - country, 6 - date-of-birth, 7 - organization, 8 - position, 9 - other-info> <new-value> - edits a note\"");
                return;
            }
            int id;
            if (!Int32.TryParse(args[0], out id))
            {
                Console.WriteLine("Id must be a number");
                return;
            }
            Note note = notebook.Find(x => x.Id == id);
            if (note == null)
            {
                Console.WriteLine($"Id {id} does not exist.");
                return;
            }
            int field;
            if (!Int32.TryParse(args[1], out field))
            {
                Console.WriteLine("Field must be a number.");
                return;
            }
            if ((field > 9) || (field < 1))
            {
                Console.WriteLine("Field must be a number from 1 to 9.");
                return;
            }
            if (field == 4)
            {
                if (IsStringANumber(args[2]))
                {
                    note.PhoneNumber = args[2];
                    Console.WriteLine($"Successfully edited field \"phone-number\" of id {id} to be {args[2]}");
                }
                else
                    Console.WriteLine("Phone number must only contain numbers.");
                return;
            }
            switch (field)
            {
                case 1:
                    note.FirstName = args[2];
                    Console.WriteLine($"Successfully edited field \"first-name\" of id {id} to be {args[2]}");
                    break;
                case 2:
                    note.LastName = args[2];
                    Console.WriteLine($"Successfully edited field \"last-name\" of id {id} to be {args[2]}");
                    break;
                case 3:
                    note.PatronymicName = args[2];
                    Console.WriteLine($"Successfully edited field \"patronymic\" of id {id} to be {args[2]}");
                    break;
                case 5:
                    note.Country = args[2];
                    Console.WriteLine($"Successfully edited field \"country\" of id {id} to be {args[2]}");
                    break;
                case 6:
                    note.DateOfBirth = args[2];
                    Console.WriteLine($"Successfully edited field \"date-of-birth\" of id {id} to be {args[2]}");
                    break;
                case 7:
                    note.Organization = args[2];
                    Console.WriteLine($"Successfully edited field \"organization\" of id {id} to be {args[2]}");
                    break;
                case 8:
                    note.Position = args[2];
                    Console.WriteLine($"Successfully edited field \"position\" of id {id} to be {args[2]}");
                    break;
                case 9:
                    note.OtherNotes = args[2];
                    Console.WriteLine($"Successfully edited field \"other-info\" of id {id} to be {args[2]}");
                    break;
            }
        }

        private static void PrintNote(List<Note> notebook, string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("Usage \"print <id>\"");
                return;
            }
            if (args.Length != 1)
            {
                Console.WriteLine("Usage \"print <id>\"");
                return;
            }
            int id;
            if (!Int32.TryParse(args[0], out id))
            {
                Console.WriteLine("Id must be a number");
                return;
            }
            Note note = notebook.Find(x => x.Id == id);
            if (note == null)
                Console.WriteLine($"Id {id} does not exist.");
            else
                Console.WriteLine(note.DetailedInfo());
        }

        private static void PrintNoteList(List<Note> notebook, string[] args)
        {
            if (args != null)
            {
                Console.WriteLine("Usage \"printlist\"");
                return;
            }
            foreach (Note n in notebook)
                Console.WriteLine(n);
        }

        private static void DeleteNote(List<Note> notebook, string[] args)
        {
            if (args == null)
            {
                Console.WriteLine("Usage \"delete <id>\"");
                return;
            }
            if (args.Length != 1)
            {
                Console.WriteLine("Usage \"delete <id>\"");
                return;
            }
            int id;
            if (!Int32.TryParse(args[0], out id))
            {
                Console.WriteLine("Id must be a number");
                return;
            }
            notebook.Remove(notebook.Find(x => x.Id == id));
        }

        private static void PrintHelp(string[] args)
        {
            if (args != null)
            {
                Console.WriteLine("Usage \"help\"");
                return;
            }
            Console.WriteLine("new <first-name> <last-name> [patronymic] <phone-number> <country> [date-of-birth, organization, position, other-info] - creates a new note");
            Console.WriteLine("edit <id> <field: 1 - first-name, 2 - last-name, 3 - patronymic, 4 - phone-number, 5 - country, 6 - date-of-birth, 7 - organization, 8 - position, 9 - other-info> <new-value> - edits a note");
            Console.WriteLine("delete <id> - deletes a note");
            Console.WriteLine("print <id> - displays a note");
            Console.WriteLine("printlist - displays a list of all notes");
            Console.WriteLine("help - displays this list of commands");
            Console.WriteLine("exit - terminate the program");
        }

        private static bool IsStringANumber(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if ((s[i] > '9') || (s[i] < '0'))
                    return false;
            return true;
        }
    }
}
