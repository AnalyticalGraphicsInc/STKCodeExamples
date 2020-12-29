using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Packaging;
using System.IO;
using System.IO.Compression;

namespace STK12.UIPlugin.CustomFrameEphemerisCopier
{
    public static class Zip
    {
        private const long BUFFER_SIZE = 4096;

        public static void AddFileToZip(string zipFilename, string fileToAdd)
        {
            using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
            {
                string destFilename = ".\\" + Path.GetFileName(fileToAdd);
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri))
                {
                    zip.DeletePart(uri);
                }
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
                using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
                {
                    using (Stream dest = part.GetStream())
                    {
                        CopyStream(fileStream, dest);
                    }
                }
            }
        }

        private static void CopyStream(System.IO.FileStream inputStream, System.IO.Stream outputStream)
        {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesWritten += bufferSize;
            }
        }



        public static void UnzipFiles(string zipLocation, string directory)
        {
            ZipPackage zipFile = ZipPackage.Open(zipLocation, FileMode.Open) as ZipPackage;
            foreach (ZipPackagePart part in zipFile.GetParts())
            {
                using (Stream source = part.GetStream(FileMode.Open, FileAccess.Read))
                {
                    FileStream targetFile = File.OpenWrite(Path.Combine(directory, part.Uri.OriginalString.TrimStart('/')));
                    source.CopyTo(targetFile);
                    targetFile.Close();
                }
            }
        }

        public static void Decompress(FileInfo fi)
        {
            // Get the stream of the source file. 
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension,  
                // for example "doc" from report.doc.cmp.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length - fi.Extension.Length);
                // inFile.ReadByte();
                // inFile.ReadByte();
                //Create the decompressed file. 
                using (FileStream outFile = File.Create(origName))
                {
                    using (DeflateStream Decompress = new DeflateStream(inFile, CompressionMode.Decompress))
                    {

                        //Copy the decompression stream into the output file.
                        byte[] buffer = new byte[4096];
                        int numRead;
                        while ((numRead = Decompress.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            outFile.Write(buffer, 0, numRead);
                        }

                    }
                }
            }
        }
    }
}
