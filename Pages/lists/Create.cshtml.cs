using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore. Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ToDo_List.Pages.lists
{
    public class CreateModel : PageModel
    {
        public ToDo list = new ToDo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            list.name = Request.Form["name"];
            list.descricao = Request.Form["descricao"];
            list.prazo = Request.Form["prazo"];

            if (list.name.Length == 0 || list.descricao.Length == 0 || list.prazo.Length == 0)
            {
                errorMessage = "Preencha todos os campos";
                return;
            }
            else
            {
                //salvar nova lista
                try
                {
                    String connectionString = "Data Source=OCOISARUIM;Initial Catalog=Crud;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                    using (SqlConnection connection = new SqlConnection(connectionString)) 
                    {
                        connection.Open();
                        String sql = "INSERT INTO list" +
                                     "(nome, descrição,prazo) VALUES " +
                                     "(@name, @descricao, @prazo);";
                        using (SqlCommand command = new SqlCommand(sql, connection)) 
                        {
                            command.Parameters.AddWithValue("@name",list.name);
                            command.Parameters.AddWithValue("@descricao", list.descricao);
                            command.Parameters.AddWithValue("@prazo", list.prazo);

                            command.ExecuteNonQuery();  
                        }
                    }
                }
                catch (Exception ex) {

                    errorMessage = ex.Message;
                    return;
                }

                list.name = ""; list.descricao = ""; list.prazo = "";
                successMessage = "Nova tarefa adcionada";

                Response.Redirect("/lists/Index");
            }
        }
    }
}
