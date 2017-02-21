using System.Collections.Specialized;
using System.Configuration;

namespace FileShare.AmazonS3
{
    public class AmazonConfiguration
    {

        /*private readonly NameValueCollection amazonConfigSection = (NameValueCollection)ConfigurationManager.GetSection("AmazonConfig");
        public string BucketName => amazonConfigSection["AmazonBucketName"];
        public string AWSAccessKey => amazonConfigSection["AWSAccessKey"];
        public string AWSSecretKey => amazonConfigSection["AWSSecretKey"];*/

        public string BucketName => ConfigurationManager.AppSettings["AmazonBucketName"];
        public string AWSAccessKey => ConfigurationManager.AppSettings["AWSAccessKey"];
        public string AWSSecretKey => ConfigurationManager.AppSettings["AWSSecretKey"];
    }
}