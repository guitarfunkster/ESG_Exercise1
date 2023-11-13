using System.Text;

namespace ESG_Exercise1
{
    public class NegativesNotAllowedException : Exception
    {
        public NegativesNotAllowedException() { }

        public NegativesNotAllowedException(List<int> invalidValues)
    :       base(String.Format("Negatives not allowed: {0}", ConvertToMessage(invalidValues)))
        {

        }
        private static string ConvertToMessage(List<int> negatives)
        {

            StringBuilder builder = new StringBuilder();
            foreach (int negative in negatives)
            {
                builder.Append(negative);   
            }
      
            return builder.ToString();

        }

    }
}
