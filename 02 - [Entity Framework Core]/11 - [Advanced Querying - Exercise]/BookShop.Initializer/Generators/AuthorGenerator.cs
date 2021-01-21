using BookShop.Models;

namespace BookShop.Initializer.Generators
{
    internal class AuthorGenerator
    {
        internal static Author[] CreateAuthors()
        {
            var authorNames = new string[]
            {
                "Nayden Vitanov",
                "Deyan Tanev",
                "Desislav Petkov",
                "Dyakon Hristov",
                "Milen Todorov",
                "Aleksander Kishishev",
                "Ilian Stoev",
                "Milen Balkanski",
                "Kostadin Nakov",
                "Petar Strashilov",
                "Bozhidara Valova",
                "Lyubina Kostova",
                "Radka Antonova",
                "Vladimira Blagoeva",
                "Bozhidara Rysinova",
                "Borislava Dimitrova",
                "Anelia Velichkova",
                "Violeta Kochanova",
                "Lyubov Ivanova",
                "Blagorodna Dineva",
                "Desislav Bachev",
                "Mihael Borisov",
                "Ventsislav Petrova",
                "Hristo Kirilov",
                "Penko Dachev",
                "Nikolai Zhelyaskov",
                "Petar Tsvetanov",
                "Spas Dimitrov",
                "Stanko Popov",
                "Miro Kochanov",
                "Pesho Stamatov",
                "Roger Porter",
                "Jeffrey Snyder",
                "Louis Coleman",
                "George Powell",
                "Jane Ortiz",
                "Randy Morales",
                "Lisa Davis",

            };

            var authorsCount = authorNames.Length;

            var authors = new Author[authorsCount];

            for (int i = 0; i < authorsCount; i++)
            {
                var authorNameTokens = authorNames[i]
                    .Split();

                var author = new Author
                {
                    FirstName = authorNameTokens[0],
                    LastName = authorNameTokens[1]
                };

                authors[i] = author;
            }

            return authors;
        }
    }
}