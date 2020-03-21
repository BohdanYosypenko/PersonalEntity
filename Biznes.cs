using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enti
{
    

    public class Biznes<T> where T : Emploee
    {

        public string Name { get; private set; }

        public Biznes(string name)
        {
            this.Name = name;
        }

        T newEmploee = null;

        public void Add()
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                newEmploee = new Emploee() as T;

                Console.WriteLine("Вкажіть ім'я працівниика");
                newEmploee.FirstName = Console.ReadLine();

                Console.WriteLine("Вкажіть прізвище працівниика");
                newEmploee.SecondName = Console.ReadLine();

                Console.WriteLine("Вкажіть дату народження працівниика");
                newEmploee.DateOfBirth = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Вкажіть посаду працівниика");
                newEmploee.Status = Console.ReadLine();

                Console.WriteLine("Вкажіть відділ працівниика");
                newEmploee.Department = Console.ReadLine();

                Console.WriteLine("Вкажіть номер кімнати працівниика");
                newEmploee.RoomNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Вкажіть телефон працівниика");
                newEmploee.Phone = Console.ReadLine();

                Console.WriteLine("Вкажіть імейл працівниика");
                newEmploee.Email = Console.ReadLine();

                Console.WriteLine("Вкажіть зарплату працівниика");
                newEmploee.Selary = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Нотатки");
                newEmploee.Notes = Console.ReadLine();


                db.Emploees.Add(newEmploee);
                db.SaveChanges();
                Console.WriteLine($"Додано нового працівника. Id працівника");
                Show(newEmploee.Id);
            }

        }

        public void DeleteAll()
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                try
                {

                    db.Database.EnsureDeleted();                    
                    db.Database.EnsureCreated();
                    
                }
                catch { throw new Exception("База даних відсутня"); }
            }

        }
        public void DeleteDb(int id)
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                Emploee emploee1 =  FindEmploeeDB(id);
                if (emploee1 == null)
                    throw new Exception("Рахунок не знайдено");
                else
                {
                    db.Emploees.Remove(emploee1);
                    db.SaveChanges();
                }
            }
        }


        public  void Edit(int id)
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                Emploee emploee = FindEmploeeDB(id);
                if (emploee == null)
                {
                    throw new Exception("Рахунок не знайдено");
                }

                Console.WriteLine("Виберіть номер інформацї яку потрібно змінити");
                Console.WriteLine("1. Ім'я\n" +
                "2. Фамілія\n" +
                "3. Дата народження\n" +
                "4. Статус\n" +
                "5. Департамент\n" +
                "6. номер кімнати\n" +
                "7. Телефон\n" +
                "8. Емейл\n" +
                "9. Зарплата\n" +
                "10. Нотатки\n");
                int number = Convert.ToInt32(Console.ReadLine());
                try
                {

                    switch (number)
                    {
                        case 1:
                            Console.WriteLine("Вкажіть нове Ім'я");
                            emploee.FirstName = Console.ReadLine();
                            
                            break;
                        case 2:
                            Console.WriteLine("Вкажіть нову фамілію");
                            emploee.SecondName = Console.ReadLine();
                           
                            break;
                        case 3:
                            Console.WriteLine("Вкажіть нову дату народження");
                            emploee.DateOfBirth = DateTime.Parse(Console.ReadLine());
                            
                            break;
                        case 4:
                            Console.WriteLine("Вкажіть новий посаду");
                            emploee.Status = Console.ReadLine();
                            db.SaveChanges();
                            break;
                        case 5:
                            Console.WriteLine("Вкажіть новий департамент");
                            emploee.Department = Console.ReadLine();
                            
                            break;
                        case 6:
                            Console.WriteLine("Вкажіть новий номер кімнати");
                            emploee.RoomNumber = Convert.ToInt32(Console.ReadLine());
                            
                            break;
                        case 7:
                            Console.WriteLine("Вкажіть нове Ім'я");
                            emploee.Phone = Console.ReadLine();
                            db.SaveChanges();
                            break;
                        case 8:
                            Console.WriteLine("Вкажіть нову Електронну пошту");
                            emploee.Email = Console.ReadLine();
                           
                            break;
                        case 9:
                            Console.WriteLine("Вкажіть нову зарплатню");
                            emploee.Selary = Convert.ToInt32(Console.ReadLine());
                            
                            break;
                        case 10:
                            Console.WriteLine("Вкажіть нову нотатку");
                            emploee.Notes = Console.ReadLine();
                            
                            break;
                    }
                    db.Emploees.Update(emploee);
                    db.SaveChanges();
                    Show(emploee.Id);

                }

                catch
                {
                    throw new Exception("Невірно вказано номер");
                }
            }
        }

        public  void Show(int id)
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                Emploee emploee =  FindEmploeeDB(id);
                if (emploee == null)
                    throw new Exception("Працівника не знайдено");
                Emploee[] ep = new Emploee[] { emploee };
                ShowAll(ep);
            }
        }

        public T FindEmploeeDB(int id)
        {

            using (helloappdbContext db = new helloappdbContext())
            {
                T findEmpl =  db.Emploees.FirstOrDefault(e => e.Id == id) as T;
                return findEmpl;
                
            }
            
        }



        public void ShowAll(Emploee[] emploees)
        {
            string textId = "ID";
            string textFirstName = "Ім'я";
            string textSecondName = "Фамілія";
            string textDateofBirth = "Дата народження";
            string textStatus = "Посада";
            string textDepartment = "Департамент";
            string textRommNumber = "Кімната";
            string textPhone = "Телефон";
            string textEmail = "Імейл";
            string textSelary = "Зарплата";
            string textNotes = "Нотатки";

            int idLenght, n, s, dofb, stat, dep, numb, phon, mail, many, note;
            idLenght = textId.Length;
            n = textFirstName.Length;
            s = textSecondName.Length;
            dofb = textDateofBirth.Length;
            stat = textStatus.Length;
            dep = textDepartment.Length;
            numb = textRommNumber.Length;
            phon = textPhone.Length;
            mail = textEmail.Length;
            many = textSelary.Length;
            note = textNotes.Length;


            foreach (Emploee e in emploees)
            {

                if (e.Id.ToString().Length > idLenght)
                {
                    idLenght = e.Id.ToString().Length;
                }
                if (e.FirstName.Length > n)
                {
                    n = e.FirstName.Length;
                }
                if (e.SecondName.Length > s)
                {
                    s = e.SecondName.Length;
                }
                if (e.DateOfBirth.ToShortDateString().Length > dofb)
                {
                    dofb = e.DateOfBirth.ToShortDateString().Length;
                }

                if (e.Status.Length > stat)
                {
                    stat = e.Status.Length;

                }

                if (e.Department.Length > dep)
                {
                    dep = e.Department.Length;
                }


                if (Convert.ToString(e.RoomNumber).Length > numb)
                {
                    numb = Convert.ToString(e.RoomNumber).Length;
                }
                if (e.Phone.Length > phon)
                {
                    phon = e.Phone.Length;
                }
                if (e.Email.Length > mail)
                {
                    mail = e.Email.Length;
                }
                if (Convert.ToString(e.Selary).Length > many)
                {
                    many = Convert.ToString(e.Selary).Length;
                }
                if (e.Notes.Length > note)
                {
                    note = e.Notes.Length;
                }


            }

            string space = new String(' ', n - textFirstName.Length + 2);
            string spaces = new String(' ', s - textSecondName.Length + 2);
            string spacedofb = new String(' ', dofb - textDateofBirth.Length + 2);
            string statSpaces = new String(' ', stat - textStatus.Length + 2);
            string depSpaces = new String(' ', dep - textDepartment.Length + 2);
            string numbSpaces = new String(' ', numb - textRommNumber.Length + 2);
            string phonSpaces = new String(' ', phon - textPhone.Length + 2);
            string mailSpaces = new String(' ', mail - textEmail.Length + 2);
            string manySpaces = new String(' ', many - textSelary.Length + 2);
            string noteSpaces = new String(' ', note - textNotes.Length + 2);

            string Lines = new String('-', n + s + dofb + stat + dep + numb + phon + mail + many + note + 46);
            string textPersonalInfo = String.Format($"| {textId.PadRight(idLenght, ' ')} | {textFirstName.PadRight(n, ' ')}  | {textSecondName.PadRight(s, ' ')}  | {textDateofBirth.PadRight(dofb, ' ')}  | {textStatus.PadRight(stat, ' ')}" +
                $"  | {textDepartment.PadRight(dep, ' ')}  | {textRommNumber.PadRight(numb, ' ')}  | {textPhone.PadRight(phon, ' ')}  | {textEmail.PadRight(mail, ' ')}  | {textSelary.PadRight(many, ' ')}  | {textNotes.PadRight(note, ' ')}  |");
            
            Console.WriteLine(Lines);
            Console.WriteLine(textPersonalInfo);  
            Console.WriteLine(Lines);


            foreach (Emploee e in emploees)
            {

                string personalInfo = String.Format($"| {e.Id.ToString().PadRight(idLenght, ' ')} | {e.FirstName.PadRight(n, ' ')}  | {e.SecondName.PadRight(s, ' ')}  | {e.DateOfBirth.ToShortDateString().PadRight(dofb, ' ')}  | {e.Status.PadRight(stat, ' ')}" +
                $"  | {e.Department.PadRight(dep, ' ')}  | {e.RoomNumber.ToString().PadRight(numb, ' ')}  | {e.Phone.PadRight(phon, ' ')}  | {e.Email.PadRight(mail, ' ')}  | {e.Selary.ToString().PadRight(many, ' ')}  | {e.Notes.PadRight(note, ' ')}  |");

                Console.WriteLine(personalInfo);
                Console.WriteLine(Lines);
            }


        }

        public Emploee[] ShowEmploees(string parametr)
        {
            using (helloappdbContext db = new helloappdbContext())
            {
                string value;

                Emploee[] emploees = db.Emploees.ToArray();
                switch (parametr)
                {
                    case "1":
                        Console.WriteLine("Введіть ім'я працівника");
                        value = Console.ReadLine();
                        var selectedEmploees = from empl in emploees
                                               where empl.FirstName.ToUpper().StartsWith(value.ToUpper())
                                               select empl;

                        if (selectedEmploees.Count() > 0)
                        {

                            return selectedEmploees.ToArray();
                        }
                        else
                        {
                            Console.WriteLine("Працівника з даним ім'ям не знайдено");
                            return selectedEmploees.ToArray();
                        }
                        break;

                    case "2":
                        Console.WriteLine("Введіть фамілію працівника");
                        value = Console.ReadLine();
                        selectedEmploees = from empl in emploees
                                           where empl.SecondName.ToUpper().StartsWith(value.ToUpper())
                                           select empl;

                        if (selectedEmploees.Count() > 0)
                            return selectedEmploees.ToArray();
                        else
                        {
                            Console.WriteLine("Працівника з даною фамілією не знайдено");
                            return selectedEmploees.ToArray();
                        }
                        break;
                    case "3":
                        Console.WriteLine("Введіть вік працівника");
                        value = Console.ReadLine();
                        selectedEmploees = from empl in emploees
                                           where (DateTime.Now.Year - empl.DateOfBirth.Year) == Convert.ToInt32(value)
                                           select empl;

                        if (selectedEmploees.Count() > 0)
                            return selectedEmploees.ToArray();
                        else
                        {
                            Console.WriteLine($"Працівника віком {value} не знайдено");
                            return selectedEmploees.ToArray();
                        }
                        break;
                    case "4":
                        Console.WriteLine("Введіть посаду на якій працює даний працівник");
                        value = Console.ReadLine();
                        selectedEmploees = from empl in emploees
                                           where empl.Status.ToUpper().StartsWith(value.ToUpper())
                                           select empl;

                        if (selectedEmploees.Count() > 0)
                            return selectedEmploees.ToArray();
                        else
                        {
                            Console.WriteLine("Працівника з даною посадою не знайдено");
                            return selectedEmploees.ToArray();
                        }
                        break;

                    case "5":
                        Console.WriteLine("Введіть відділ у якому працює даний працівник");
                        value = Console.ReadLine();
                        selectedEmploees = from empl in emploees
                                           where empl.Department.ToUpper().StartsWith(value.ToUpper())
                                           select empl;

                        if (selectedEmploees.Count() > 0)
                            return selectedEmploees.ToArray();
                        else
                        {
                            Console.WriteLine("Працівника не знайдено");
                            return selectedEmploees.ToArray();
                        }
                        break;

                    case "6":
                        Console.WriteLine("Введіть номер кімнати у якому знаходиться працівник");
                        value = Console.ReadLine();
                        selectedEmploees = from empl in emploees
                                           where empl.RoomNumber == Convert.ToInt32(value)
                                           select empl;

                        if (selectedEmploees.Count() > 0)
                            return selectedEmploees.ToArray();
                        else
                        {
                            Console.WriteLine("Працівника не знайдено");
                            return selectedEmploees.ToArray();
                        }
                        break;

                        int minValue, maxValue;
                    case "7":
                        Console.WriteLine("Введіть діапазон пшуку зарплати");
                        Console.Write("Мінімальне значення - ");
                        minValue = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Максимальне значення - ");
                        maxValue = Convert.ToInt32(Console.ReadLine());
                        selectedEmploees = from empl in emploees
                                           where Convert.ToInt32(empl.Selary) > minValue && Convert.ToInt32(empl.Selary) < maxValue
                                           select empl;

                        if (selectedEmploees.Count() > 0)
                            return selectedEmploees.ToArray();
                        else
                        {
                            Console.WriteLine("Працівника не знайдено");
                            return selectedEmploees.ToArray();
                        }
                        break;

                    default:
                        return null;
                        break;


                }




            }
        }
    }
}
