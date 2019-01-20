using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerGamesShop.Models.Seed
{
    public class SeedGames
    {
        public static Game[] games = new Game[]
        {
            new Game { ID = 1, Title = "Dragon Age Inquisition", Description = " You are the Inquisitor, tasked with saving the people of Thedas from the brink of chaos and a host of fearsome enemies.",
                Genre = Genre.Fantasy, Image = "Images/games/DRAGON AGE.jpg", IsMultiplayer=true,
                Price = 60, PublisherID=10, ReleaseDate = DateTime.Parse("2015-10-6") },
            new Game { ID = 2, Title = "Hearthstone", Description = "Sheathe your sword, draw your deck, and get ready for Hearthstone – the fast paced strategy card game that’s easy to learn and massively fun. You’ll play your cards to sling spells, summon creatures, and command the heroes of Warcraft in duels of epic strategy.",
                Genre = Genre.Cards, Image = "Images/games/HEARTHSTONE.jpg", IsMultiplayer=true,
                Price = 20, PublisherID=3, ReleaseDate = DateTime.Parse("2014-06-11") },
            new Game { ID = 3, Title = "FIFA 18", Description = "Powered by Frostbite, FIFA 18 blurs the line between the virtual and real worlds, bringing to life the players, teams and atmospheres of The World’s Game.",
                Genre = Genre.Sports, Image = "Images/games/FIFA.jpg", IsMultiplayer=true,
                Price = 35, PublisherID=10, ReleaseDate = DateTime.Parse("2017-09-29") },
            new Game { ID = 4, Title = "Assassins Creed Origins", Description = "ASSASSIN’S CREED® ORIGINS IS A NEW BEGINNING Ancient Egypt, a land of majesty and intrigue, is disappearing in a ruthless fight for power.",
                Genre = Genre.Adventure, Image = "Images/games/ASSASSINS CREED.jpg", IsMultiplayer=false,
                Price = 95, PublisherID=1, ReleaseDate = DateTime.Parse("2017-10-27") },
            new Game { ID = 5, Title = "Battlefield 1", Description = "Battlefield™ 1 takes you back to The Great War, WW1, where new technology and worldwide conflict changed the face of warfare forever.",
                Genre = Genre.Action, Image = "Images/games/BATTLEFIELD.png", IsMultiplayer=true,
                Price = 40, PublisherID=10, ReleaseDate = DateTime.Parse("2010-03-02") },
            new Game { ID = 6, Title = "Game of Thrones", Description = "Game of Thrones - A Telltale Games Series is a six part episodic game series set in the world of HBO's groundbreaking TV show. This new story tells of House Forrester, a noble family from the north of Westeros, loyal to the Starks of Winterfell.",
                Genre = Genre.Choices, Image = "Images/games/GAME OF THRONES.jpg", IsMultiplayer=false,
                Price = 25, PublisherID=7, ReleaseDate = DateTime.Parse("2014-12-02") },
            new Game { ID = 7, Title = "Portal 2", Description = "Portal 2 draws from the award-winning formula of innovative gameplay, story, and music that earned the original Portal over 70 industry accolades and created a cult following.",
                Genre = Genre.Platformer, Image = "Images/games/Portal_2.jpg", IsMultiplayer=true,
                Price = 50, PublisherID=8, ReleaseDate = DateTime.Parse("2011-04-19") },
            new Game { ID = 8, Title = "Project CARS", Description = "Project CARS is the ultimate driver journey!",
                Genre = Genre.Racing, Image = "Images/games/PROJECT CARS.jpg", IsMultiplayer=true,
                Price = 55, PublisherID=5, ReleaseDate = DateTime.Parse("2015-03-16") },
            new Game { ID = 9, Title = "Rocket League", Description = "Soccer meets driving once again in the long-awaited, physics-based multiplayer-focused sequel to Supersonic Acrobatic Rocket-Powered Battle-Cars! ",
                Genre = Genre.Racing, Image = "Images/games/ROCKET LEAGUE.jpg", IsMultiplayer=true,
                Price = 20, PublisherID=9, ReleaseDate = DateTime.Parse("2015-07-07") },
            new Game { ID = 10, Title = "Rollercoaster Tycoon 2", Description = "RollerCoaster Tycoon 2 is a construction and management simulation computer game that simulates amusement park management.",
                Genre = Genre.Strategy, Image = "Images/games/ROLLERCOASTER.jpg", IsMultiplayer=false,
                Price = 5, PublisherID=4, ReleaseDate = DateTime.Parse("2002-10-03") },
            new Game { ID = 11, Title = "Starcraft 2", Description = "Welcome the Koprulu sector. You are Jim Raynor, and you're on a crusade to bring down the Dominion and its nefarious leader, Arcturus Mengsk. Haunted by betrayal and remorse, some believe you've given up the fight. But you have promises to keep – and a need for vengeance that’s long overdue.",
                Genre = Genre.Strategy, Image = "Images/games/StarCraft_II_-_Box_Art.jpg", IsMultiplayer=true,
                Price = 20, PublisherID=3, ReleaseDate = DateTime.Parse("2010-07-27") },
            new Game { ID = 12, Title = "The Elder Scrools V: Skyrim", Description = "Winner of more than 200 Game of the Year Awards, Skyrim Special Edition brings the epic fantasy to life in stunning detail. The Special Edition includes the critically acclaimed game and add-ons with all-new features like remastered art and effects, volumetric god rays, dynamic depth of field, screen-space reflections, and more.",
                Genre = Genre.Fantasy, Image = "Images/games/The_Elder_Scrolls_V_Skyrim_cover.png", IsMultiplayer=false,
                Price = 45, PublisherID=8, ReleaseDate = DateTime.Parse("2011-11-11") },
            new Game { ID = 13, Title = "The Sims 4", Description = "Play with Life. Create the lives you have always wanted! Ready to live a freer, more creative life? In The Sims, you can let your fantasies run wild as you design your ideal world. Start with your Sim, refining each shape, color and personality trait until you get the precise person that pleases you.",
                Genre = Genre.Strategy, Image = "Images/games/THE_SIMS_4.jpg", IsMultiplayer=false,
                Price = 80, PublisherID=10, ReleaseDate = DateTime.Parse("2017-09-26") },
            new Game { ID = 14, Title = "Grand Theft Auto V", Description = "Elevate your criminal empire with GTA Online: Smuggler’s Run. Manage a new smuggling business from customizable Hangar properties and store your collection of new planes and choppers. Featuring seven new aircraft, two new cars, Motor Wars Adversary Mode and much more.",
                Genre = Genre.Action, Image = "Images/games/GTAV.png", IsMultiplayer=true,
                Price = 80, PublisherID=6, ReleaseDate = DateTime.Parse("2015-04-14") }
        };

        public static void InitialGames(IServiceProvider serviceProvider)
        {
            using (var context = new ComputerGamesShopContext(serviceProvider.GetRequiredService<DbContextOptions<ComputerGamesShopContext>>()))
            {
                context.Database.OpenConnection();
                try
                {
                    context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Game ON");
                    context.SaveChanges();
                    context.Database.ExecuteSqlCommand("DELETE FROM dbo.Game");
                    context.Game.AddRange(games);
                    context.SaveChanges();
                }
                finally
                {
                    context.Database.CloseConnection();
                }
            }
        }
    }
}
