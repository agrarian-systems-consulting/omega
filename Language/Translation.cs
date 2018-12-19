using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA
{
    /// <summary>
    /// On gère la traduction avec un fichier .csv qui contient tous les mots dans toutes les langues.
    /// </summary>
    static class Translation
    {

        internal static string Translate(string Defaultword, int langue)
        {
            string translatedWord;
            if (Defaultword.Length < 1 )
            {
                //MessageBox.Show("Translation error, unknown word !");
                return Defaultword;
            }

            if (langue > 5)
            {
                //MessageBox.Show("Translation error, unknown language !");
                return Defaultword;
            }

            translatedWord = FindTranslation(Defaultword, langue);
        
            if (translatedWord.Length < 1)
            {
                //MessageBox.Show("Translation error, unknown word !");
                return Defaultword;
            }
            return translatedWord;
         }
        /// <summary>
        /// Moins de vérification, afin de gagner du temps
        /// </summary>
        /// <param name="originalword"></param>
        /// <param name="langueDest"></param>
        /// <returns></returns>
        internal static string QuickTranslate(string originalword, int langueDest)
        {
            string translatedWord = originalword;
            try
            {
                StreamReader TranslateFile = new StreamReader(Properties.Settings.Default.FichierTraduction);
                string LigneToCheck = TranslateFile.ReadLine();
                while (TranslateFile.EndOfStream == false)
                {
                    LigneToCheck = TranslateFile.ReadLine();
                    if (LigneToCheck.First() != '#')
                    {
                        if (LigneToCheck.Contains(originalword))
                        {
                            string[] ArrTranslateWord = LigneToCheck.Split(';');
                            translatedWord = ArrTranslateWord[langueDest];
                        }
                    }
                }
                TranslateFile.Close();
                TranslateFile.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return translatedWord;
        }

        private static string FindTranslation(string word,int langue)
        {
            try
            {
                if (Properties.Settings.Default.FichierTraduction == "") return word;
                string original_word = word;
                StreamReader TranslateFile = new StreamReader(Properties.Settings.Default.FichierTraduction);
                string LigneToCheck = TranslateFile.ReadLine();
                while (TranslateFile.EndOfStream == false)
                {
                    LigneToCheck = TranslateFile.ReadLine();
                    if (LigneToCheck.First() != '#')
                    {
                        string[] ArrTranslateWord = LigneToCheck.Split(';');
                        if (word == ArrTranslateWord[0])
                        {
                            word = ArrTranslateWord[langue];
                            break;
                        }
                        
                    }
                }
                TranslateFile.Close();
                TranslateFile.Dispose();
                if (original_word == word) //Log.WriteLog("No translated word : " + word);
                if (word.Length < 1) Log.WriteLog("No translaation of '"+ original_word + "' in language   : " + langue);
            }
            catch (Exception e)
            {
                if (e.Message == "Sequence contains no elements")
                {
                    MessageBox.Show("the word "+ word + " has no translation available in this language.");
                }
                else
                MessageBox.Show(e.Message);
            }
           return word;
        }


    }
}
