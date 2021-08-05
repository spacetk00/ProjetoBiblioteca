namespace Biblioteca.Models
{
    public class Usuario
    {
        //ADMIN = 0
        //PADRAO = 1
        public static int Id {get; set;}
        public string Nome {get; set;}
        public string Login {get; set;}
        public string Senha {get; set;}
        public int tipo {get; set;}
    }
}