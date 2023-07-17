using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DocumentDistance
{
    class DocDistance
    {
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            // TODO comment the following line THEN fill your code here
            //throw new NotImplementedException();
            String text1 = File.ReadAllText(doc1FilePath);
            String text2 = File.ReadAllText(doc2FilePath);
            //int check;
            //List<double> totalfreq_1  = new List<double>();
            

            double result;
            //split each doc into words
            var punctuation1 =text1.Where(Char.IsSymbol).Distinct().ToArray();

            /*char[] punctuation1 = { ' ', ',', '.', ':', '\t', '\'', ';', '&','@','$','(',')' ,'"'
                   ,'*','\r',  '\n','|','_','-' ,'(',')' ,'?','!','%' , '^','+','=','0','1', '2', '3',
            '4','5' , '6' , '7' , '8' ,'9'};*/
            var punctuation = text2.Where(char.IsSymbol).Distinct().ToArray();

            //words=String.Concat(words.Where(words[i] => !Char.IsWhiteSpace(c)));
            String[] words = text1.Split(punctuation1,StringSplitOptions.RemoveEmptyEntries);
           String[] words2 = text2.Split(punctuation, StringSplitOptions.RemoveEmptyEntries);
            
            //compute frequency
            double[] totalfreq_1 = new double[words.Count()];
            double[] totalfreq_2 = new double[words2.Count()];
            Frequencies(words).CopyTo(totalfreq_1,0);
            Frequencies(words2).CopyTo(totalfreq_2,0);

            //compute distance
            double product = 0.0;
            double d1 = 0.0, d2 = 0.0;
            for (int i = 0; i < words.Count() && i < words2.Count(); i++)
            {
                var test_text2 = text2.ToLower();
                var test = words[i].ToLower();
                double value;
                if ( test_text2.Contains(test) == true )
                {
                    value = getfrequency(words[i], words2, totalfreq_2);
                    product += (value * totalfreq_1.ElementAt(i));
                }
                d1 += Math.Pow(totalfreq_1.ElementAt(i), 2);
                d2 += Math.Pow(totalfreq_2.ElementAt(i), 2); 
            }
            result = Math.Acos(product / Math.Sqrt(Math.Abs(d1) * Math.Abs(d2)));
            result = result * 180 / Math.PI;
            return result;
        }

        public static double[] Frequencies(string[] words)
        {
            int count;
            // int check;
            double[] totalfreq = new double[words.Count()];
            //count word frequencies
            
            for (int i = 0; i < words.Count(); i++)
            {
                // Console.WriteLine(words[i]);
               
                count = 1;
                for (int j = i + 1; j < words.Count(); j++)
                {
                    bool check = String.Equals(words[i], words[j], StringComparison.CurrentCultureIgnoreCase);
                    if (check == true)
                        count++;
                }
                totalfreq[i] = count;
                //Console.WriteLine(totalfreq[i]);
            }
            return totalfreq;
        }
        
        public static double getfrequency ( string word ,string [] arr, double [] freq)
        {
            double freq_value = 0;
            for(int i =0; i< arr.Length; i++)
            {
              bool  check = String.Equals(word, arr[i], StringComparison.CurrentCultureIgnoreCase);
                if (check == true)
                {
                    freq_value = freq.ElementAt(i);
                    break;
                }
            }
            return freq_value;
        }
    }
}
