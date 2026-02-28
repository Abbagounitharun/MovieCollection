// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System;
using System.Collections.Generic;
public delegate void MovieAckDelegate(string message);//delegate is created.
class Movie
{
    public Guid id { get; set; }//Properties are declared.  Example for changes
    public string title { get; set; }
    public string actor { get; set; }
    public string actress { get; set; }
    public string director { get; set; }

    public int year { get; set; }
   public Movie(Guid id, string title, string actor, string actress, string director, int year)//Properties are initialized.
    {
        this.id = id;
        this.title = title;
        this.actor = actor;
        this.actress = actress;
        this.director = director;
        this.year = year;
    }
}

class MovieCollection
{
   private string language { get; set; } 
   public Dictionary<Guid,Movie> MoviesCollections=new Dictionary<Guid,Movie>();//MoviesCollections stores Guid and Movie object
    MovieManager movieManager1=new MovieManager();
    MovieAckDelegate ack = new MovieAckDelegate(MovieManager.MovieAcknowledgement);//Reference is given to Delegate
   public MovieCollection(string language)
    {
        this.language=language; 
    }
   public void AddMovie(Movie movie)
    {
        if (movie == null)
            throw new ArgumentNullException(nameof(movie));

        if (MoviesCollections.ContainsKey(movie.id))
            throw new InvalidOperationException("Movie already exists");

        MoviesCollections.Add(movie.id, movie);
        ack.Invoke($"{language}: Movie {movie.title} is added in Movie Collections with id {movie.id}");//Delegate is invoked.

    }

   public void RemoveMovie(Guid id)
    {
        //if (id == null)
          //  throw new ArgumentNullException(nameof(id));
        //MovieCollection collection=new MovieCollection();
        string title=MoviesCollections[id].title;
        if (MoviesCollections.ContainsKey(id))
        {
            MoviesCollections.Remove(id);
        }
        else
        {
            throw new KeyNotFoundException("Movie not found");
        }
        ack.Invoke($"{language}: Movie {title} is removed from Movie Collections");

    }

   public void MovieCount()
    {
        Console.WriteLine($"{language}: Total Movies: {MoviesCollections.Count}");

    }
    
    public void GetMovies(Dictionary<Guid,Movie> MoviesCollections)
    {
        Console.WriteLine($"Language is {language}");
        foreach(var movie in MoviesCollections)
        {
            Console.WriteLine($"Movie id is {movie.Key} ");
            Console.WriteLine($"Movie name is {movie.Value.title}");
            Console.WriteLine($"Year of published is {movie.Value.year}");
            Console.WriteLine($"Actor name is {movie.Value.actor}");
            Console.WriteLine($"Actress name is {movie.Value.actress}");
            Console.WriteLine($"Director name is {movie.Value.director}");
        }
    }
}

class MovieManager
{
   
    public static void Main()
    {
       // MovieManager movieManager = new MovieManager();
        MovieCollection currentCollection= null;
        Dictionary<int,MovieCollection> language= new Dictionary<int,MovieCollection>();//Dictionary is created to store the language index and Object of specific language.
       
        while (true)
        {
        Console.WriteLine("--------Select Language of the movie--------");
        Console.WriteLine("1.Telugu");
        Console.WriteLine("2.Hindi");
        Console.WriteLine("3.English");
        int selectedLanguage=int.Parse(Console.ReadLine());
        if(!language.TryGetValue(selectedLanguage, out currentCollection))
            {
                string langName = selectedLanguage switch
                {
                    1 => "Telugu",
                    2 => "Hindi",
                    3 => "English",
                    _ => "select correct option"


                };
                currentCollection = new MovieCollection(langName);
                language[selectedLanguage] = currentCollection;
                Console.WriteLine($"{langName} collection created.");
            }
        else
            {
                Console.WriteLine("Using existing Collection");
            }
                Console.WriteLine("-------Select the one option from below------");
        Console.WriteLine("1.Add Movie");
        Console.WriteLine("2.Remove Movie");
        Console.WriteLine("3.Count Movie Collections");
        Console.WriteLine("4.Get all movies");
        int option=int.Parse(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter movie Name:");
                    string movieName = Console.ReadLine();
                    Console.WriteLine("Enter actor name:");
                    string actor = Console.ReadLine();
                    Console.WriteLine("Enter actress name:");
                    string actress = Console.ReadLine();
                    Console.WriteLine("Enter director name:");
                    string director = Console.ReadLine();
                    Console.WriteLine("Enter year of released:");
                    int year = int.Parse(Console.ReadLine());
                    Movie movie = new Movie(Guid.NewGuid(), movieName, actor, actress, director, year);
                    currentCollection.AddMovie(movie);
                    break;
                case 2:
                    Guid id =  Guid.Parse(Console.ReadLine());
                    currentCollection.RemoveMovie(id);
                    break;
                case 3:
                    currentCollection.MovieCount();
                    break;
                case 4:
                    currentCollection.GetMovies(currentCollection.MoviesCollections);
                    break;
                case -1:
                    return;
                default:
                    Console.WriteLine("Please select valid option!!!");
                    break;

            }
        }

    }
    public static void MovieAcknowledgement(string message)
    {
        Console.WriteLine(message);

    }
}
