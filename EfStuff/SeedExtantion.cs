using LearnEnglish.EfStuff.Model;
using LearnEnglish.EfStuff.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.EfStuff
{
    public static class SeedExtantion
    {
        public const string Admin = "admin";

        public const string Moderator = "moderator";

        public static IHost Seed(this IHost host)
        {
            using (var service = host.Services.CreateScope())
            {
                InitUsers(service.ServiceProvider);

                InitBankCards(service.ServiceProvider);

                InitCourse(service.ServiceProvider);

                InitBook(service.ServiceProvider);
            }

            return host;
        }

        private static void InitBook(IServiceProvider service)
        {
            var bookRepository = service.GetService<BookRepository>();
            var userRepository = service.GetService<UserRepository>();
            var admin = userRepository.Get(Admin);
            var bookDefaults = new List<Book>() {
                new Book()
                {
                    Name= "Book1",
                    Url = "https://i.pinimg.com/736x/75/1d/4b/751d4bda81598c27a15ac46874b3a305.jpg"
                },
                new Book()
                {
                    Name= "Book2",
                    Url = "https://i.pinimg.com/originals/2c/86/5d/2c865d628ff955fbc87e1ab106236dab.jpg"
                }
            };

            foreach (var book in bookDefaults)
            {
                if (!bookRepository.Exist(book.Name))
                {
                    book.Creater = admin;
                    bookRepository.Save(book);
                }
            }
        }

        private static void InitUsers(IServiceProvider service)
        {
            var userRepository = service.GetService<UserRepository>();

            if (!userRepository.Exist(Admin))
            {
                var admin = new User()
                {
                    Login = Admin,
                    Password = "admin",
                    Role = Role.Admin,
                    Language = Language.Russian
                };

                userRepository.Save(admin);
            }

            if (!userRepository.Exist(Moderator))
            {
                var admin = new User()
                {
                    Login = Moderator,
                    Password = "moderator",
                    Role = Role.Moderator,
                    Language = Language.Russian
                };

                userRepository.Save(admin);
            }

            var userDefaults = new List<User>()
            {
                new User()
                {
                    Login = "tom",
                    Password = "123",
                    Role = Role.User,
                    Language = Language.English,
                },
                new User()
                {
                    Login = "nik",
                    Password = "123",
                    Role = Role.User,
                    Language = Language.Polish,
                },
            };

            foreach (var user in userDefaults)
            {
                if (!userRepository.Exist(user.Login))
                {
                    userRepository.Save(user);
                }
            }
        }

        private static void InitBankCards(IServiceProvider service)
        {
            var bankCardRepository = service.GetService<BankCardRepository>();
            var userRepository = service.GetService<UserRepository>();
            var admin = userRepository.Get(Admin);

            var bankCardDefaults = new List<BankCard>() {
                new BankCard()
                {
                    CardNumber = "4855777788889997",
                    ValidityMonth = 02,
                    ValidityYear = 2022
                },
                new BankCard()
                {
                    CardNumber = "4888999977776667",
                    ValidityMonth = 05,
                    ValidityYear = 2025
                },
                new BankCard()
                {
                    CardNumber = "4333222211112222",
                    ValidityMonth = 11,
                    ValidityYear = 2024
                }
            };

            foreach (var bankCard in bankCardDefaults)
            {
                if (!bankCardRepository.Exist(bankCard.CardNumber))
                {
                    bankCard.Owner = admin;
                    bankCardRepository.Save(bankCard);
                }
            }
        }

        private static void InitCourse(IServiceProvider service)
        {
            var courseRepository = service.GetService<LessonRepository>();

            var courseDefaults = new List<Lesson>() {
                new Lesson()
                {
                    Name = "Grammar. Level Biginner.",
                },
                new Lesson()
                {
                    Name = "Grammar. Level Middle.",
                },
                new Lesson()
                {
                    Name = "Grammar. Level High.",
                },
                new Lesson()
                {
                    Name = "Conversation. Level Biginner.",
                },
                new Lesson()
                {
                    Name = "Conversation. Level Middle.",
                },
                new Lesson()
                {
                    Name = "Conversation. Level High.",
                },
            };

            foreach (var course in courseDefaults)
            {
                if (!courseRepository.Exist(course.Name))
                {
                    courseRepository.Save(course);
                }
            }
        }
    }
}