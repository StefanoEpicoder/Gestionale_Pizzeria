namespace BE.U2_W3_D5.PS_PIZZERIA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;

    [Table("USER")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            DETTAGLIO = new HashSet<DETTAGLIO>();
            ORDINE = new HashSet<ORDINE>();

        }

        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string Pass { get; set; }

        [Required]
        [StringLength(50)]
        public string Ruolo { get; set; }


        //AUTENTICAZIONE
        public static bool Autenticato(string username, string password)
        {
            SqlConnection con = Connessioni.GetConnection();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [USER] WHERE Username = @username and [Pass]=@Password", con);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("Password", password);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Errore durante l'autenticazione: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETTAGLIO> DETTAGLIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDINE> ORDINE { get; set; }
    }
}
