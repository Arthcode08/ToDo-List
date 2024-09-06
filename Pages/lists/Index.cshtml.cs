using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ToDo_List.Pages.lists
{
    public class IndexModel : PageModel
    {
        public List<ToDo> list = new List<ToDo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=OCOISARUIM;Initial Catalog=Crud;Integrated Security=True;Trust Server Certificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM  list";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) 
                            {
                                ToDo toDo = new ToDo();
                                toDo.Id = "" + reader.GetInt32(0);
                                toDo.name = reader.GetString(1);
                                toDo.descricao = reader.GetString(2);
                                toDo.prazo = reader.GetString(3);
                                toDo.criada = reader.GetDateTime(4).ToString();

                                list.Add(toDo); 

                            }
                        }
                    }
                }
            }
            catch 
            {
                Console.WriteLine("Aconteceu algo inesperado : " + ToString());
            }
        }
    }
    public class ToDo
    {
        public String Id;
        public String name;
        public String descricao;
        public String criada;
        public String prazo;
    }
}
