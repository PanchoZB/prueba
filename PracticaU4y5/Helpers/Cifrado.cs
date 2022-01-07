using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PracticaU4y5.Helpers
{
    public class Cifrado
    {
        public static string GetHash(string cadena)
        {
           

            var algoritmo = SHA512.Create();
           
            var arreglo = Encoding.UTF8.GetBytes(cadena);

            var hash = algoritmo.ComputeHash(arreglo).Select(x => x.ToString("x2"));

            return string.Join("", hash);
        }
    }
}
