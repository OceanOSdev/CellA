using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellular_Automata
{
    public class CellAutomata
    {
        #region Variables
        private byte _rule;
        private Dictionary<char[], char> ruleDefinition = new Dictionary<char[], char>();
        private string _cells;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the Cellular Automata class
        /// </summary>
        /// <param name="rule">CA rule number</param>
        /// <param name="cellData">the one dimensional cell layout in binary</param>
        /// <example>"0001000"</example>
        public CellAutomata(byte rule, string cellData)
        {
            _rule = rule;
            _cells = cellData;
            InitDictionary();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Fills the ruleDefinition dictionary
        /// </summary>
        private void InitDictionary()
        {
            char[] binRule = Convert.ToString(_rule, 2).PadLeft(8, '0').ToCharArray();  // gets binary representation of the rule number
            for (int i = 0; i < binRule.Length; i++)
            {
                binRule[i] = binRule[i] == '1' ? 'x' : ' ';
            }

            for (int i = 0; i < binRule.Length; i++)
            {
                char[] bin = Convert.ToString((7 - i), 2).PadLeft(3, '0').ToCharArray();
                for(int j = 0; j < bin.Length; j++)
                {
                    bin[j] = (bin[j] == '1') ? 'x': ' ';
                }
                ruleDefinition.Add(bin, binRule[i]); // adds rules to dictionary
            }

            

        }
        /// <summary>
        /// Converts the first generation into printable form that is also readable by the next generation
        /// </summary>
        /// <param name="generation">the initial generation in binary</param>
        /// <returns>printable form</returns>
        private string CalculateFirstGeneration(string generation)
        {
            char[] toPattern = generation.ToCharArray();
            for (int i = 0; i < toPattern.Length; i++)
            {
                toPattern[i] = toPattern[i] == '1' ? 'x' : ' ';
            }

            return new string(toPattern);
        }

        /// <summary>
        /// Calculates next generation of cellular automata
        /// </summary>
        /// <param name="generation">the previous generation</param>
        /// <returns>the next generation</returns>
        private string CalculateNextGeneration(string generation)
        {
            string nextGeneration = string.Concat(generation, "  ");            // pads the right with two spaces (0s)
            char[] nextGenArray = nextGeneration.ToCharArray();                 // creates an array of chars for easier string manipulation
            for (int i = 0; i < generation.Length; i++)
            {
                char[] section = nextGeneration.Substring(i, 3).ToCharArray();  // gets the three cell section
                char newMiddle = ' ';                                           // the middle cell in the next generation
                foreach (var item in ruleDefinition)
                {
                    bool eq = equal(item.Key, section);
                    if (eq)
                    {
                        newMiddle = item.Value;
                        break;
                    }
                }

                nextGenArray[i + 1] = newMiddle;
            }
            return new string(nextGenArray).Substring(0, generation.Length);
        }

        /// <summary>
        /// The == operator didn't work, so now i have to do exactly what it does
        /// except that when i do it manually it will work for some reason
        /// </summary>
        /// <param name="arrayOne">one of the arrays</param>
        /// <param name="arrayTwo">the other array</param>
        /// <returns>whether or not they are equivalent</returns>
        private bool equal(char[] arrayOne, char[] arrayTwo)
        {
            bool ret = false;

            for (int i = 0; i < arrayOne.Length; i++)
            {
                if (arrayOne[i] == arrayTwo[i])
                    ret = true;
                else
                {
                    ret = false;
                    break;
                }
            }

            return ret;

        }

        /// <summary>
        /// generates the list of outputs from each generation
        /// </summary>
        /// <param name="generations">the number of generations to calculate</param>
        /// <returns>the list of generations as strings</returns>
        public List<string> Generations(int generations)
        {
            List<string> generationList = new List<string>();
            string cellData = CalculateFirstGeneration(Cells);
            generationList.Add(cellData);
            for (int g = 0; g < generations; g++)
            {
                cellData = CalculateNextGeneration(cellData);
                generationList.Add(cellData);
            }

            return generationList;
        }
        #endregion

        #region Properties
        public string Cells
        {
            get
            {
                return _cells;
            }

            set
            {
                _cells = value;
            }
        }
        #endregion

    }
}
