using System;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Collections;
using System.Collections.Generic;

namespace OMEGA
{

       /// <summary>
       /// Classe non utilisée
       /// </summary>

    class XPathNav
    {
        private string mXMLFilePath;

        internal XPathNav(string FullpathXMLfile)
        {
            mXMLFilePath = FullpathXMLfile;
        }

        /// <summary>
        /// Récupère la valeur de l'attribut du noeud recherché dans le fichier de configuration
        /// </summary>
        /// <param name="xPathString">Expression XPath de recherche du noeud</param>
        /// <param name="attribute">Attribut à rechercher</param>
        /// <returns>Une ArrayList contenant la liste des attributs recherchés</returns>
        internal ArrayList SearchXPathNavigator(string xPathString, string attribute)
        {
            // Initilisation des variables
            XPathDocument xpathDoc;
            XPathNavigator xpathNavigator;
            XPathNodeIterator xpathNodeIterator;
            XPathExpression expr;
            ArrayList listOfAttributes = new ArrayList();
            List<string> list = new List<string>();

            // Parcours du fichier XML
            try
            {
                xpathDoc = new XPathDocument(mXMLFilePath);
                xpathNavigator = xpathDoc.CreateNavigator();

                expr = xpathNavigator.Compile(xPathString);
                xpathNodeIterator = xpathNavigator.Select(expr);

                while (xpathNodeIterator.MoveNext())
                {
                    // On récupère l'attribut
                    listOfAttributes.Add(xpathNodeIterator.Current.GetAttribute(attribute,""));
                    list.Add(xpathNodeIterator.Current.Value);
                }
            }
            catch (Exception e)
            {
            }
            return listOfAttributes;
        }


        internal string SearchXPathNavigator(string xPathString)
        {
            // Initilisation des variables
            XPathDocument xpathDoc;
            XPathNavigator xpathNavigator;
            XPathNodeIterator xpathNodeIterator;
            XPathExpression expr;
            ArrayList listOfAttributes = new ArrayList();
            List<string> list = new List<string>();

            // Parcours du fichier XML
            try
            {
                xpathDoc = new XPathDocument(mXMLFilePath);
                xpathNavigator = xpathDoc.CreateNavigator();

                expr = xpathNavigator.Compile(xPathString);
                xpathNodeIterator = xpathNavigator.Select(expr);

                if (xpathNodeIterator.MoveNext())
                { 
                    return xpathNodeIterator.Current.Value;
                }
            }
            catch (Exception e)
            {
            }
            return "";
        }
    }
}

