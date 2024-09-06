using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace ToDo_List.Pages.lists
{
    public class EditModel : PageModel
    {
        public lists list = new lists();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=OCOISARUIM;Initial Catalog=Crud;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT *FROM clients WHERE id =@id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                list.id = "" + reader.GetInt32(0);
                                list.name ="" + reader.GetString(1);
                                list.descricao = reader.GetString(2);
                                list.prazo = reader.GetString(3);
                            }
                        }

                    }
                }

            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() 
        {
            list.id = Request.Query["id"];
            list.name = Request.Query["name"];
            list.descricao = Request.Query["desc"];
            list.prazo = Request.Query["prazo"];

            if (list.id.Lenght == 0 || list.name.Lenght == 0 || list.descricao.Lenght == 0 || list.prazo.Length == 0)  
            {
                errorMessage = "Preencha todos os campos";
                return;

            }
            try
            {

            }
            catch (Exception ex) 
            {
                errorMessage = ex.Message;
                return
            }
        }
    }
}
