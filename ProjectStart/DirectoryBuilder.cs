using System;
using System.IO;
using System.Threading;
using System.Net;

namespace ProjectStart
{
    public class DirectoryBuilder
    {
        string currentDir;

        string basePath;
        public string BasePath
        {
            get { return basePath; }
            set { basePath = value; }
        }

        public DirectoryBuilder()
        {

        }

        public void AddDirectory(string dir, ContentTemplate template)
        {
            currentDir = Path.Combine(basePath, dir);
            Directory.CreateDirectory(currentDir);

            if (template != null)
            {
                if (template.Mode == ContentTemplateMode.FolderContents)
                {

                }
                else if (template.Mode == ContentTemplateMode.Url)
                {
                    Thread t = new Thread(new ThreadStart(delegate()
                    {
                        string openDir = currentDir;
                        string url = template.Content;
                        string ext = Path.GetExtension(url);

                        try
                        {
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            byte[] buffer = new byte[4096];

                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                string file = Path.Combine(Environment.GetFolderPath(
                                    Environment.SpecialFolder.LocalApplicationData), "temp" + ext);

                                using (FileStream fs = File.Create(file))
                                {
                                    Stream s = response.GetResponseStream();
                                    int bytesRead = s.Read(buffer, 0, 4096);
                                    fs.Write(buffer, 0, bytesRead);

                                    s.Close();
                                    fs.Close();
                                }
                            }

                            response.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        if (ext == ".zip")
                        {
                            //Unzip contents to folder "dir"	
                        }
                    }));
                }
            }
        }

        public void AddFile(string file, ContentTemplate template)
        {
            string path = Path.Combine(currentDir, file);

            if (template != null)
            {
                if (template.Mode == ContentTemplateMode.CustomText)
                {
                    File.WriteAllText(path, template.Content);
                }
                else if (template.Mode == ContentTemplateMode.FileContents)
                {
                    string sourceFile = template.Content;

                    if (!File.Exists(sourceFile))
                    {
                        File.Create(path);
                        throw new FileNotFoundException(string.Format("The file {0} does not exist in {1}",
                            Path.GetFileName(sourceFile), Path.GetDirectoryName(sourceFile)));
                    }
                    else
                    {
                        File.WriteAllText(path, File.ReadAllText(sourceFile));
                    }
                }
            }
            else
                File.Create(path);
        }
    }
}

