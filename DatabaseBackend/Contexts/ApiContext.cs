using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary;
using SharedLibrary.Models;

namespace DatabaseBackend {
    public class ApiContext : DbContext {

        public ApiContext() : base() {

        }

        public ApiContext( [NotNullAttribute] DbContextOptions options ) : base( options ) {

            // this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<EventOwnership> EventOwnerships { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<EventProgram> EventPrograms { get; set; }

        public DbSet<EventReview> EventReviews { get; set; }

        public DbSet<EventRegistration> EventRegistrations { get; set; }

        private static readonly string[] ipsum = "lorem ipsum dolor sit amet consectetuer adipiscing elit aenean commodo ligula eget dolor aenean massa cum sociis natoque penatibus et magnis dis parturient montes nascetur ridiculus mus donec quam felis ultricies nec pellentesque eu pretium quis sem nulla consequat massa quis enim donec pede justo fringilla vel aliquet nec vulputate eget arcu in enim justo rhoncus ut imperdiet a venenatis vitae justo nullam dictum felis eu pede mollis pretium integer tincidunt cras dapibus vivamus elementum semper nisi aenean vulputate eleifend tellus aenean leo ligula porttitor eu consequat vitae eleifend ac enim aliquam lorem ante dapibus in viverra quis feugiat a tellus phasellus viverra nulla ut metus varius laoreet quisque rutrum aenean imperdiet etiam ultricies nisi vel augue curabitur ullamcorper ultricies nisi nam eget dui etiam rhoncus maecenas tempus tellus eget condimentum rhoncus sem quam semper libero sit amet adipiscing sem neque sed ipsum nam quam nunc blandit vel luctus pulvinar hendrerit id lorem maecenas nec odio et ante tincidunt tempus donec vitae sapien ut libero venenatis faucibus nullam quis ante etiam sit amet orci eget eros faucibus tincidunt duis leo sed fringilla mauris sit amet nibh donec sodales sagittis magna sed consequat leo eget bibendum sodales augue velit cursus nunc quis gravida magna mi a libero fusce vulputate eleifend sapien vestibulum purus quam scelerisque ut mollis sed nonummy id metus nullam accumsan lorem in dui cras ultricies mi eu turpis hendrerit fringilla vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; in ac dui quis mi consectetuer lacinia nam pretium turpis et arcu duis arcu tortor suscipit eget imperdiet nec imperdiet iaculis ipsum sed aliquam ultrices mauris integer ante arcu accumsan a consectetuer eget posuere ut mauris praesent adipiscing phasellus ullamcorper ipsum rutrum nunc nunc nonummy metus vestibulum volutpat pretium libero cras id dui aenean ut eros et nisl sagittis vestibulum nullam nulla eros ultricies sit amet nonummy id imperdiet feugiat pede sed lectus donec mollis hendrerit risus phasellus nec sem in justo pellentesque facilisis etiam imperdiet imperdiet orci nunc nec neque phasellus leo dolor tempus non auctor et hendrerit quis nisi curabitur ligula sapien tincidunt non euismod vitae posuere imperdiet leo maecenas malesuada praesent congue erat at massa sed cursus turpis vitae tortor donec posuere vulputate arcu phasellus accumsan cursus velit vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; sed aliquam nisi quis porttitor congue elit erat euismod orci ac placerat dolor lectus quis orci phasellus consectetuer vestibulum elit aenean tellus metus bibendum sed posuere ac mattis non nunc vestibulum fringilla pede sit amet augue in turpis pellentesque posuere praesent turpis aenean posuere tortor sed cursus feugiat nunc augue blandit nunc eu sollicitudin urna dolor sagittis lacus donec elit libero sodales nec volutpat a suscipit non turpis nullam sagittis suspendisse pulvinar augue ac venenatis condimentum sem libero volutpat nibh nec pellentesque velit pede quis nunc vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; fusce id purus ut varius tincidunt libero phasellus dolor maecenas vestibulum mollis diam pellentesque ut neque pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas in dui magna posuere eget vestibulum et tempor auctor justo in ac felis quis tortor malesuada pretium pellentesque auctor neque nec urna proin sapien ipsum porta a auctor quis euismod ut mi aenean viverra rhoncus pede pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas ut non enim eleifend felis pretium feugiat vivamus quis mi phasellus a est phasellus magna in hac habitasse platea dictumst curabitur at lacus ac velit ornare lobortis curabitur a felis in nunc fringilla tristique morbi mattis ullamcorper velit phasellus gravida semper nisi nullam vel sem pellentesque libero tortor tincidunt et tincidunt eget semper nec quam sed hendrerit morbi ac felis nunc egestas augue at pellentesque laoreet felis eros vehicula leo at malesuada velit leo quis pede donec interdum metus et hendrerit aliquet dolor diam sagittis ligula eget egestas libero turpis vel mi nunc nulla fusce risus nisl viverra et tempor et pretium in sapien donec venenatis vulputate lorem morbi nec metus phasellus blandit leo ut odio maecenas ullamcorper dui et placerat feugiat eros pede varius nisi condimentum viverra felis nunc et lorem sed magna purus fermentum eu tincidunt eu varius ut felis in auctor lobortis lacus quisque libero metus condimentum nec tempor a commodo mollis magna vestibulum ullamcorper mauris at ligula fusce fermentum nullam cursus lacinia erat praesent blandit laoreet nibh fusce convallis metus id felis luctus adipiscing pellentesque egestas neque sit amet convallis pulvinar justo nulla eleifend augue ac auctor orci leo non est quisque id mi ut tincidunt tincidunt erat etiam feugiat lorem non metus vestibulum dapibus nunc ac augue curabitur vestibulum aliquam leo praesent egestas neque eu enim in hac habitasse platea dictumst fusce a quam etiam ut purus mattis mauris sodales aliquam curabitur nisi quisque malesuada placerat nisl nam ipsum risus rutrum vitae vestibulum eu molestie vel lacus sed augue ipsum egestas nec vestibulum et malesuada adipiscing dui vestibulum facilisis purus nec pulvinar iaculis ligula mi congue nunc vitae euismod ligula urna in dolor mauris sollicitudin fermentum libero praesent nonummy mi in odio nunc interdum lacus sit amet orci vestibulum rutrum mi nec elementum vehicula eros quam gravida nisl id fringilla neque ante vel mi morbi mollis tellus ac sapien phasellus volutpat metus eget egestas mollis lacus lacus blandit dui id egestas quam mauris ut lacus fusce vel dui sed in libero ut nibh placerat accumsan proin faucibus arcu quis ante in consectetuer turpis ut velit nulla sit amet est praesent metus tellus elementum eu semper a adipiscing nec purus cras risus ipsum faucibus ut ullamcorper id varius ac leo suspendisse feugiat suspendisse enim turpis dictum sed iaculis a condimentum nec nisi praesent nec nisl a purus blandit viverra praesent ac massa at ligula laoreet iaculis nulla neque dolor sagittis eget iaculis quis molestie non velit mauris turpis nunc blandit et volutpat molestie porta ut ligula fusce pharetra convallis urna quisque ut nisi donec mi odio faucibus at scelerisque quis convallis in nisi suspendisse non nisl sit amet velit hendrerit rutrum ut leo ut a nisl id ante tempus hendrerit proin pretium leo ac pellentesque mollis felis nunc ultrices eros sed gravida augue augue mollis justo suspendisse eu ligula nulla facilisi donec id justo praesent porttitor nulla vitae posuere iaculis arcu nisl dignissim dolor a pretium mi sem ut ipsum curabitur suscipit suscipit tellus praesent vestibulum dapibus nibh etiam iaculis nunc ac metus ut id nisl quis enim dignissim sagittis etiam sollicitudin ipsum eu pulvinar rutrum tellus ipsum laoreet sapien quis venenatis ante odio sit amet eros proin magna duis vel nibh at velit scelerisque suscipit curabitur turpis vestibulum suscipit nulla quis orci fusce ac felis sit amet ligula pharetra condimentum maecenas egestas arcu quis ligula mattis placerat duis lobortis massa imperdiet quam suspendisse potenti pellentesque commodo eros a enim vestibulum turpis sem aliquet eget lobortis pellentesque rutrum eu nisl sed libero aliquam erat volutpat etiam vitae tortor morbi vestibulum volutpat enim aliquam eu nunc nunc sed turpis sed mollis eros et ultrices tempus mauris ipsum aliquam libero non adipiscing dolor urna a orci nulla porta dolor class aptent taciti sociosqu ad litora torquent per conubia nostra per inceptos hymenaeos pellentesque dapibus hendrerit tortor praesent egestas tristique nibh sed a libero cras varius donec vitae orci sed dolor rutrum auctor fusce egestas elit eget lorem suspendisse nisl elit rhoncus eget elementum ac condimentum eget diam nam at tortor in tellus interdum sagittis aliquam lobortis donec orci lectus aliquam ut faucibus non euismod id nulla curabitur blandit mollis lacus nam adipiscing vestibulum eu odio vivamus laoreet nullam tincidunt adipiscing enim phasellus tempus proin viverra ligula sit amet ultrices semper ligula arcu tristique sapien a accumsan nisi mauris ac eros fusce neque suspendisse faucibus nunc et pellentesque egestas lacus ante convallis tellus vitae iaculis lacus elit id tortor vivamus aliquet elit ac nisl fusce fermentum odio nec arcu vivamus euismod mauris in ut quam vitae odio lacinia tincidunt praesent ut ligula non mi varius sagittis cras sagittis praesent ac sem eget est egestas volutpat vivamus consectetuer hendrerit lacus cras non dolor vivamus in erat ut urna cursus vestibulum fusce commodo aliquam arcu nam commodo suscipit quam quisque id odio praesent venenatis metus at tortor pulvinar varius".Split(' ');
        private static readonly Random random = new Random();
        private static readonly string[] names = new string[] { "Noah", "Liam", "Mason", "Jacob", "William", "Ethan", "James", "Alexander", "Michael", "Benjamin", "Elijah", "Daniel", "Aiden", "Logan", "Matthew", "Lucas", "Jackson", "David", "Oliver", "Jayden", "Joseph", "Gabriel", "Samuel", "Carter", "Anthony", "John", "Dylan", "Luke", "Henry", "Andrew", "Isaac", "Christopher", "Joshua", "Wyatt", "Sebastian", "Owen", "Caleb", "Nathan", "Ryan", "Jack", "Hunter", "Levi", "Christian", "Jaxon", "Julian", "Landon", "Grayson", "Jonathan", "Isaiah", "Charles", "Thomas", "Aaron", "Eli", "Connor", "Jeremiah", "Cameron", "Josiah", "Adrian", "Colton", "Jordan", "Brayden", "Nicholas", "Robert", "Angel", "Hudson", "Lincoln", "Evan", "Dominic", "Austin", "Gavin", "Nolan", "Parker", "Adam", "Chase", "Jace", "Ian", "Cooper", "Easton", "Kevin", "Jose", "Tyler", "Brandon", "Asher", "Jaxson", "Mateo", "Jason", "Ayden", "Zachary", "Carson", "Xavier", "Leo", "Ezra", "Bentley", "Sawyer", "Kayden", "Blake", "Nathaniel", "Ryder", "Theodore", "Elias", "Tristan", "Roman", "Leonardo", "Camden", "Brody", "Luis", "Miles", "Micah", "Vincent", "Justin", "Greyson", "Declan", "Maxwell", "Juan", "Cole", "Damian", "Carlos", "Max", "Harrison", "Weston", "Brantley", "Braxton", "Axel", "Diego", "Abel", "Wesley", "Santiago", "Jesus", "Silas", "Giovanni", "Bryce", "Jayce", "Bryson", "Alex", "Everett", "George", "Eric", "Ivan", "Emmett", "Kaiden", "Ashton", "Kingston", "Jonah", "Jameson", "Kai", "Maddox", "Timothy", "Ezekiel", "Ryker", "Emmanuel", "Hayden", "Antonio", "Bennett", "Steven", "Richard", "Jude", "Luca", "Edward", "Joel", "Victor", "Miguel", "Malachi", "King", "Patrick", "Kaleb", "Bryan", "Alan", "Marcus", "Preston", "Abraham", "Calvin", "Colin", "Bradley", "Jeremy", "Kyle", "Graham", "Grant", "Jesse", "Kaden", "Alejandro", "Oscar", "Jase", "Karter", "Maverick", "Aidan", "Tucker", "Avery", "Amir", "Brian", "Iker", "Matteo", "Caden", "Zayden", "Riley", "August", "Mark", "Maximus", "Brady", "Kenneth", "Paul", "Jaden", "Nicolas", "Beau", "Dean", "Jake", "Peter", "Xander", "Elliot", "Finn", "Derek", "Sean", "Cayden", "Elliott", "Jax", "Jasper", "Lorenzo", "Omar", "Beckett", "Rowan", "Gael", "Corbin", "Waylon", "Myles", "Tanner", "Jorge", "Javier", "Zion", "Andres", "Charlie", "Paxton", "Emiliano", "Brooks", "Zane", "Simon", "Judah", "Griffin", "Cody", "Gunner", "Dawson", "Israel", "Rylan", "Gage", "Messiah", "River", "Kameron", "Stephen", "Francisco", "Clayton", "Zander", "Chance", "Eduardo", "Spencer", "Lukas", "Damien", "Dallas", "Conner", "Travis", "Knox", "Raymond", "Peyton", "Devin", "Felix", "Jayceon", "Collin", "Amari", "Erick", "Cash", "Jaiden", "Fernando", "Cristian", "Josue", "Keegan", "Garrett", "Rhett", "Ricardo", "Martin", "Reid", "Seth", "Andre", "Cesar", "Titus", "Donovan", "Manuel", "Mario", "Caiden", "Adriel", "Kyler", "Milo", "Archer", "Jeffrey", "Holden", "Arthur", "Karson", "Rafael", "Shane", "Lane", "Louis", "Angelo", "Remington", "Troy", "Emerson", "Maximiliano", "Hector", "Emilio", "Anderson", "Trevor", "Phoenix", "Walter", "Johnathan", "Johnny", "Edwin", "Julius", "Barrett", "Leon", "Tyson", "Tobias", "Edgar", "Dominick", "Marshall", "Marco", "Joaquin", "Dante", "Andy", "Cruz", "Ali", "Finley", "Dalton", "Gideon", "Reed", "Enzo", "Sergio", "Jett", "Thiago", "Kyrie", "Ronan", "Cohen", "Colt", "Erik", "Trenton", "Jared", "Walker", "Landen", "Alexis", "Nash", "Jaylen", "Gregory", "Emanuel", "Killian", "Allen", "Atticus", "Desmond", "Shawn", "Grady", "Quinn", "Frank", "Fabian", "Dakota", "Roberto", "Beckham", "Major", "Skyler", "Nehemiah", "Drew", "Cade", "Muhammad", "Kendrick", "Pedro", "Orion", "Aden", "Kamden", "Ruben", "Zaiden", "Clark", "Noel", "Porter", "Solomon", "Romeo", "Rory", "Malik", "Daxton", "Leland", "Kash", "Abram", "Derrick", "Kade", "Gunnar", "Prince", "Brendan", "Leonel", "Kason", "Braylon", "Legend", "Pablo", "Jay", "Adan", "Jensen", "Esteban", "Kellan", "Drake", "Warren", "Ismael", "Ari", "Russell", "Bruce", "Finnegan", "Marcos", "Jayson", "Theo", "Jaxton", "Phillip", "Dexter", "Braylen", "Armando", "Braden", "Corey", "Kolton", "Gerardo", "Ace", "Ellis", "Malcolm", "Tate", "Zachariah", "Chandler", "Milan", "Keith", "Danny", "Damon", "Enrique", "Jonas", "Kane", "Princeton", "Hugo", "Ronald", "Philip", "Ibrahim", "Kayson", "Maximilian", "Lawson", "Harvey", "Albert", "Donald", "Raul", "Franklin", "Hendrix", "Odin", "Brennan", "Jamison", "Dillon", "Brock", "Landyn", "Mohamed", "Brycen", "Deacon", "Colby", "Alec", "Julio", "Scott", "Matias", "Sullivan", "Rodrigo", "Cason", "Taylor", "Rocco", "Nico", "Royal", "Pierce", "Augustus", "Raiden", "Kasen", "Benson", "Moses", "Cyrus", "Raylan", "Davis", "Khalil", "Moises", "Conor", "Nikolai", "Alijah", "Mathew", "Keaton", "Francis", "Quentin", "Ty", "Jaime", "Ronin", "Kian", "Lennox", "Malakai", "Atlas", "Jerry", "Ryland", "Ahmed", "Saul", "Sterling", "Dennis", "Lawrence", "Zayne", "Bodhi", "Arjun", "Darius", "Arlo", "Eden", "Tony", "Dustin", "Kellen", "Chris", "Mohammed", "Nasir", "Omari", "Kieran", "Nixon", "Rhys", "Armani", "Arturo", "Bowen", "Frederick", "Callen", "Leonidas", "Remy", "Wade", "Luka", "Jakob", "Winston", "Justice", "Alonzo", "Curtis", "Aarav", "Gustavo", "Royce", "Asa", "Gannon", "Kyson", "Hank", "Izaiah", "Roy", "Raphael", "Luciano", "Hayes", "Case", "Darren", "Mohammad", "Otto", "Layton", "Isaias", "Alberto", "Jamari", "Colten", "Dax", "Marvin", "Casey", "Moshe", "Johan", "Sam", "Matthias", "Larry", "Trey", "Devon", "Trent", "Mauricio", "Mathias", "Issac", "Dorian", "Gianni", "Ahmad", "Nikolas", "Oakley", "Uriel", "Lewis", "Randy", "Cullen", "Braydon", "Ezequiel", "Reece", "Jimmy", "Crosby", "Soren", "Uriah", "Roger", "Nathanael", "Emmitt", "Gary", "Rayan", "Ricky", "Mitchell", "Roland", "Alfredo", "Cannon", "Jalen", "Tatum", "Kobe", "Yusuf", "Quinton", "Korbin", "Brayan", "Joe", "Byron", "Ariel", "Quincy", "Carl", "Kristopher", "Alvin", "Duke", "Lance", "London", "Jasiah", "Boston", "Santino", "Lennon", "Deandre", "Madden", "Talon", "Sylas", "Orlando", "Hamza", "Bo", "Aldo", "Douglas", "Tristen", "Wilson", "Maurice", "Samson", "Cayson", "Bryant", "Conrad", "Dane", "Julien", "Sincere", "Noe", "Salvador", "Nelson", "Edison", "Ramon", "Lucian", "Mekhi", "Niko", "Ayaan", "Vihaan", "Neil", "Titan", "Ernesto", "Brentley", "Lionel", "Zayn", "Dominik", "Cassius", "Rowen", "Blaine", "Sage", "Kelvin", "Jaxen", "Memphis", "Leonard", "Abdullah", "Jacoby", "Allan", "Jagger", "Yahir", "Forrest", "Guillermo", "Mack", "Zechariah", "Harley", "Terry", "Kylan", "Fletcher", "Rohan", "Eddie", "Bronson", "Jefferson", "Rayden", "Terrance", "Marc", "Morgan", "Valentino", "Demetrius", "Kristian", "Hezekiah", "Lee", "Alessandro", "Makai", "Rex", "Callum", "Kamari", "Casen", "Tripp", "Callan", "Stanley", "Toby", "Elian", "Langston", "Melvin", "Payton", "Flynn", "Jamir", "Kyree", "Aryan", "Axton", "Azariah", "Branson", "Reese", "Adonis", "Thaddeus", "Zeke", "Tommy", "Blaze", "Carmelo", "Skylar", "Arian", "Bruno", "Kaysen", "Layne", "Ray", "Zain", "Crew", "Jedidiah", "Rodney", "Clay", "Tomas", "Alden", "Jadiel", "Harper", "Ares", "Cory", "Brecken", "Chaim", "Nickolas", "Kareem", "Xzavier", "Kaison", "Alonso", "Amos", "Vicente", "Samir", "Yosef", "Jamal", "Jon", "Bobby", "Aron", "Ben", "Ford", "Brodie", "Cain", "Finnley", "Briggs", "Davion", "Kingsley", "Brett", "Wayne", "Zackary", "Apollo", "Emery", "Joziah", "Lucca", "Bentlee", "Hassan", "Westin", "Joey", "Vance", "Marcelo", "Axl", "Jermaine", "Chad", "Gerald", "Kole", "Dash", "Dayton", "Lachlan", "Shaun", "Kody", "Ronnie", "Kolten", "Marcel", "Stetson", "Willie", "Jeffery", "Brantlee", "Elisha", "Maxim", "Kendall", "Harry", "Leandro", "Aaden", "Channing", "Kohen", "Yousef", "Darian", "Enoch", "Mayson", "Neymar", "Giovani", "Alfonso", "Duncan", "Anders", "Braeden", "Dwayne", "Keagan", "Felipe", "Fisher", "Stefan", "Trace", "Aydin", "Anson", "Clyde", "Blaise", "Canaan", "Maxton", "Alexzander", "Billy", "Harold", "Baylor", "Gordon", "Rene", "Terrence", "Vincenzo", "Kamdyn", "Marlon", "Castiel", "Lamar", "Augustine", "Jamie", "Eugene", "Harlan", "Kase", "Miller", "Van", "Kolby", "Sonny", "Emory", "Junior", "Graysen", "Heath", "Rogelio", "Will", "Amare", "Ameer", "Camdyn", "Jerome", "Maison", "Micheal", "Cristiano", "Giancarlo", "Henrik", "Lochlan", "Bode", "Camron", "Houston", "Otis", "Hugh", "Kannon", "Konnor", "Emmet", "Kamryn", "Maximo", "Adrien", "Cedric", "Dariel", "Landry", "Leighton", "Magnus", "Draven", "Javon", "Marley", "Zavier", "Markus", "Justus", "Reyansh", "Rudy", "Santana", "Misael", "Abdiel", "Davian", "Zaire", "Jordy", "Reginald", "Benton", "Darwin", "Franco", "Jairo", "Jonathon", "Reuben", "Urijah", "Vivaan", "Brent", "Gauge", "Vaughn", "Coleman", "Zaid", "Terrell", "Kenny", "Brice", "Lyric", "Judson", "Shiloh", "Damari", "Kalel", "Braiden", "Brenden", "Coen", "Denver", "Javion", "Thatcher", "Rey", "Dilan", "Dimitri", "Immanuel", "Mustafa", "Ulises", "Alvaro", "Dominique", "Eliseo", "Anakin", "Craig", "Dario", "Santos", "Grey", "Ishaan", "Jessie", "Jonael", "Alfred", "Tyrone", "Valentin", "Jadon", "Turner", "Ignacio", "Riaan", "Rocky", "Ephraim", "Marquis", "Musa", "Keenan", "Ridge", "Chace", "Kymani", "Rodolfo", "Darrell", "Steve", "Agustin", "Jaziel", "Boone", "Cairo", "Kashton", "Rashad", "Gibson", "Jabari", "Avi", "Quintin", "Seamus", "Rolando", "Sutton", "Camilo", "Triston", "Yehuda", "Cristopher", "Davin", "Ernest", "Jamarion", "Kamren", "Salvatore", "Anton", "Aydan", "Huxley", "Jovani", "Wilder", "Bodie", "Jordyn", "Louie", "Achilles", "Kaeden", "Kamron", "Aarush", "Deangelo", "Robin", "Yadiel", "Yahya", "Boden", "Ean", "Kye", "Kylen", "Todd", "Truman", "Chevy", "Gilbert", "Haiden", "Brixton", "Dangelo", "Juelz", "Osvaldo", "Bishop", "Freddy", "Reagan", "Frankie", "Malaki", "Camren", "Deshawn", "Jayvion", "Leroy", "Briar", "Jaydon", "Antoine" };

        private static string RandomText( int count ) {

            StringBuilder builder = new StringBuilder();
            for ( int i = 0; i < count; i++ ) {

                builder.Append( ipsum[ random.Next( 0, ipsum.Length ) ] + ' ' );
            }

            return builder.ToString().TrimEnd();
        }

        private void SeedUsers( EntityTypeBuilder builder ) {

            List<User> seedData = new List<User>();

            int id = 1;
            foreach( string name in names ) {

                seedData.Add( new User() { 
                    ID = id++, 
                    Email = $"{ name.ToLower() }@aol.mail", 
                    FirstName = name, 
                    LastName = "Doe",
                    PasswordHash = "",
                    PasswordSalt = "",
                } );
            }

            builder.HasData( seedData.AsEnumerable() );
        }

        private List<Event> EventSeedData = new List<Event>();

        private void SeedEvents( EntityTypeBuilder builder ) {

            //int speakerid = names.Length + 1;
            //int programid = 1;

            for( int id = 1; id < 100; id++ ) {

                Event @event = new Event(){
                    ID = id,
                    Date = DateTime.Now.AddMinutes( -random.Next( 5259487 ) ),
                };

                @event.Title = RandomText( random.Next( 2, 5 ) );
                @event.Description = RandomText( random.Next( 20, 100 ) );
                @event.Location = RandomText( random.Next( 2, 5 ) );

                EventSeedData.Add( @event );
            }

            builder.HasData( EventSeedData.AsEnumerable() );
        }

        private void SeedSpeakers( EntityTypeBuilder builder ) {

            int id = names.Length + 1;

            List<Speaker> seedData = new List<Speaker>();

            foreach ( Event @event in EventSeedData ) {

                int count = random.Next(1,4);
                for ( int i = 0; i < count; i++ ) {

                    Speaker speaker = new Speaker( @event.ID ){

                        ID = id++,
                        FirstName = RandomText( random.Next( 1, 2 ) ),
                        LastName = RandomText( random.Next( 1, 2 ) ),
                    };

                    seedData.Add( speaker );
                }
            }

            builder.HasData( seedData.AsEnumerable() );
        }
        private void SeedPrograms( EntityTypeBuilder builder ) {

            int id = 1;

            List<EventProgram> seedData = new List<EventProgram>();

            foreach ( Event @event in EventSeedData ) {

                int count = random.Next( 3, 10 );
                for ( int i = 0; i < count; i++ ) {

                    EventProgram program = new EventProgram( @event.ID ) {
                        ID = id++,
                        Description = RandomText( random.Next( 20, 100 ) ),
                        Location = RandomText( random.Next( 2, 5 ) ),
                        Title = RandomText( random.Next( 2, 5 ) ),
                        StartTime = @event.Date.AddMinutes( random.Next(10,300) ),
                    };

                    program.EndTime = program.StartTime.AddMinutes( random.Next( 10, 120 ) );

                    seedData.Add( program );
                }
            }

            builder.HasData( seedData.AsEnumerable() );
        }

        private void SeedData( ModelBuilder modelBuilder ) {

            SeedUsers( modelBuilder.Entity<User>() );
            SeedEvents( modelBuilder.Entity<Event>() );
            SeedSpeakers( modelBuilder.Entity<Speaker>() );
            SeedPrograms( modelBuilder.Entity<EventProgram>() );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {

            // https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-3.1
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#many-to-many

            modelBuilder.Entity<User>().HasKey( o => new { o.ID } );
            modelBuilder.Entity<User>().HasAlternateKey( o => new { o.Email } ); // Add 'Email' as alternate primary key
            modelBuilder.Entity<User>().HasMany( o => o.Ownerships ).WithOne( o => o.User );
            modelBuilder.Entity<User>().HasMany( o => o.Registrations ).WithOne( o => o.User );

            modelBuilder.Entity<Event>().HasKey( o => new { o.ID } );
            modelBuilder.Entity<Event>().HasMany( o => o.Programs ).WithOne( o => o.Event );
            modelBuilder.Entity<Event>().HasMany( o => o.Ownerships ).WithOne( o => o.Event );
            modelBuilder.Entity<Event>().HasMany( o => o.Speakers ).WithOne( o => o.Event );
            modelBuilder.Entity<Event>().HasMany( o => o.Reviews ).WithOne( o => o.Event );
            modelBuilder.Entity<Event>().HasMany( o => o.Registrations ).WithOne( o => o.Event );

            SeedData( modelBuilder );
        }
    }
}
