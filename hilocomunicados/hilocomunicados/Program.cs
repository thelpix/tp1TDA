/*
 S1 = +-
->s1_total = 0
 S2 = ?-
->s2_def = -1
->s2_indef = 1

dif_s1_s2 = 0 - (-1) = 1
caminos_totales = 2
los casos donde un camino de indefiniciones, y que su suma de 1, entonces es valido (se restara y dara 0)
los casos donde un camino de indeficiones, no da 1, se guarda como fallido
caminos_validos / caminos_totales


s2 = ?-
¿como simplifico?
escogo ambas 2

   -> ? es -
        s2_copia1 = --
        s2_indef--
        una vez definido s2
        s1 - s2_copia1 != 0
        es falsa esta combinacion

  igual |-> ? es +
            if s2_copia3 está en hashtable (un char[] identico a s2_copia3, aunque no sean el mismo)
                return 0
        //se corta esto y evito sumar caminos_validos
        |    s2_copia3 = +-
             s2_indef--
        |    una vez definido s2
            ->ejecutar algoritmo que calcule
        |    s1_total == s2_total
        |    es verdadera esta combinacion
        |    caminos_validos++


igual  | -> ? es +
   |     s2_copia2 = +-
         s2_indef--
   |     una vez definida s2
        -> ejecutar algoritmo que calcule
   |     s1_total == s2_total = 0
   |     es verdadera esta combinacion
   |     caminos_validos++
        hastable(s2_copia1)


caminos_validos / caminos_totales



*/


using System;


public class Program
{
    public static void Main()
    {

        int s1_valor = 0;
        int s2_indef = 0;
        HashSet<string> caminos_ = new HashSet<string>();

        //leer el s1
        string s1 = Console.ReadLine();
        for (int i = 0; i < s1.Length; i++)
        {
            switch (s1[i]) //O(s1)
            {
                case '+':
                    s1_valor += 1;
                    break;
                case '-':
                    s1_valor -= 1;
                    break;
            }

        }

        //leer el s2
        string s2 = Console.ReadLine();
        //contador de incognitas
        for (int i = 0; i < s2.Length; i++) //O(s2)
        {
            if (s2[i] == '?') { s2_indef += 1; }
        }

        // la cantidad de combinaciones que pueden tener los indef
        int caminos_totales = (int)Math.Pow(2, s2_indef);


        float SumaCaminos(char[] s2, int k, int cantIndefinidos, HashSet<string> visitados)
        {
            float caminos_validos = 0;
            string s2_array = new string(s2); //O(S2)

            if (visitados.Contains(s2_array))
            {
                return 0;
            }

            visitados.Add(s2_array);

            //CASO BASE, que llegamos al final del s2_copia
            if (cantIndefinidos == 0)
            {
                int s2_valor = 0;
                for (int i = 0; i < s2.Length; i++) //O(s2)
                {
                    switch (s2[i])
                        {
                            case '+':
                                s2_valor++;
                                break;
                            case '-':
                                s2_valor--;
                                break;
                        }
                }

                if (s1_valor == s2_valor)
                {
                    return (caminos_validos + 1);
                }
                return caminos_validos;
            }

            //recursion

            while(k <  s2.Length && s2[k] != '?') // O(s2)
            {
                k++;
            }
            //Que el otro vecino no logre reconocer la instrucción, en cuyo caso mueve al azar, con una probabilidad de 0.5 de mover en cada dirección. 
            if (k < s2.Length && s2[k] == '?') // O(s2)
            {
                char[] s2_mas = (char[])s2.Clone(); //O(s2)
                char[] s2_menos = (char[])s2.Clone();
                s2_mas[k] = '+';
                s2_menos[k] = '-';
                //O(2^k)
                caminos_validos += SumaCaminos(s2_mas, k + 1, cantIndefinidos - 1, visitados);
                caminos_validos += SumaCaminos(s2_menos, k + 1, cantIndefinidos - 1, visitados);
            }

                return caminos_validos;
        }

        float porcentaje = (float)( (SumaCaminos(s2.ToCharArray(), 0, s2_indef, caminos_)) / caminos_totales); //O(2^k * S2)???
        Console.WriteLine(porcentaje.ToString("F13"));
        
    }
}
