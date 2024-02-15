using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM02P012024.Models;
using SQLite;

namespace PM02P012024.Controllers
{
    public class PersonasControllers

    {
        SQLiteAsyncConnection _connection;
        // Constructor Vacio
        public PersonasControllers() { }

        //Conexion a la base de datos

        async Task Init()
        {
            if (_connection is not null)
            {
                return;
            }

            SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite |
                                                  SQLite.SQLiteOpenFlags.Create   |
                                                  SQLite.SQLiteOpenFlags.SharedCache;
             
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBPersonas.db3"),extensiones);

            var creacion = await _connection.CreateTableAsync<Models.Personas>();


        }
        //Crear los metodos Crud para la clase Personas
        //Create //Update

        public async Task< int> StorePerson(Personas personas)
        {
               await Init();
            if (personas.Id == 0)
            {
                return await _connection.InsertAsync(personas);
        
            }
            else
            {
                return await _connection.UpdateAsync(personas);

            }
        }

        //Read

        public async Task<List<Models.Personas>> GetListPersons()
        {
            await Init();
            return await _connection.Table<Personas>().ToListAsync();

        }

        //Read Element

        public async Task<Models.Personas> GePerson( int pid)
        {
            await Init();
            return await _connection.Table<Personas>().Where(i => i.Id == pid ).FirstOrDefaultAsync();

        }

        //Delete

        public async Task<int> DeletePersons(Personas personas)
        {
            await Init();
            return await _connection.DeleteAsync(personas);

        }

    }
}
