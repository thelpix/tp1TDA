using System;
using System.Security.Cryptography;


public class Program
{
    public static void Main()
    {

        int s1_total = 0;
        int s2_def = 0;
        int s2_indef = 0;

        string s1 = Console.ReadLine();
        for (int i = 0; i < s1.Length; i++)
        {
            switch (s1[i])
            {
                case '+':
                    s1_total += 1;
                    break;
                case '-':
                    s1_total -= 1;
                    break;
            }

        }

        string s2 = Console.ReadLine();
        for (int i = 0; i < s2.Length; i++)
        {
            switch (s2[i])
            {
                case '+':
                    s2_def += 1;
                    break;
                case '-':
                    s2_def -= 1;
                    break;
                case '?':
                    s2_indef += 1;
                    break;
            }
        }

        //diferencia que hay (asi sabemos exactamente cual deberias ser el valor de indefinidos para que satisfaga)
        int diferencia_s1_s2 = s1_total - s2_def;
        // la cantidad de combinaciones que pueden tener los indef
        int combinaciones_indef = (int)Math.Pow(2, s2_indef);

        //simular dato
        //deberia dar 0
        float porcentajeIndefinicion(int cantIndefinidos, int diferencia)
        {
            //combinaciones indefinidas que dieron 0
            int combinaciones_indef_correctos = 0;
            Stack<int> combinaciones = new Stack<int>();
            //caso base

            //si no es valido

            //PODA, usar algun diccionario para que si la posicion a retrodecer ya probo siendo un valor, el random no lo repita, para no rehacerlo
            //recursion
            for (int i = 0; i < cantIndefinidos; i++)
            {
                Random random = new Random();
                int nRandom = random.Next(0, 1);
                //caso que ? = 1
                if (nRandom == 0)
                {
                    combinaciones.Push(1);
                }
                //caos que ? = -1
                else
                {
                    combinaciones.Push(-1);
                }

            }
            //crear una lista dinamica donde voy agregando los ? que defino random
                //ahi vuelvo hacia atras

            return combinaciones_indef / combinaciones_indef_correctos;
        }

        Console.WriteLine(porcentajeIndefinicion(s2_def, diferencia_s1_s2));
    }
}
