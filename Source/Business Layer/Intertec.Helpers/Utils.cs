﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace Intertek.Helpers
{
    public class Utils
    {
        #region Guardar Archivos

        public static bool GuardarArchivo(byte[] buffer, string pathDirectrio, string nombreArchivo)
        {
            if (!Directory.Exists(pathDirectrio))
                Directory.CreateDirectory(pathDirectrio);

            string pathArchivo = Path.Combine(pathDirectrio, nombreArchivo);

            try
            {
                var fs = new FileStream(pathArchivo, FileMode.Create, FileAccess.ReadWrite);
                var bw = new BinaryWriter(fs);
                bw.Write(buffer);
                bw.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion Guardar Archivos

        #region Formatear datos

        /// <summary>
        /// Retira todos los caracteres especiales de un texto especificado
        /// </summary>
        /// <param name="text">texto a formatear</param>
        /// <returns>retorna un texto sin caracteres especiales</returns>
        public static string RemoveIllegalCharacters(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var regex = new Regex(@"(\s|-)+", RegexOptions.Compiled);

            text = text.Replace(":", string.Empty);
            text = text.Replace("/", string.Empty);
            text = text.Replace("?", string.Empty);
            text = text.Replace("#", string.Empty);
            text = text.Replace("[", string.Empty);
            text = text.Replace("]", string.Empty);
            text = text.Replace("@", string.Empty);
            text = text.Replace("*", string.Empty);
            text = text.Replace(",", string.Empty);
            text = text.Replace(".", string.Empty);
            text = text.Replace(";", string.Empty);
            text = text.Replace("\"", string.Empty);
            text = text.Replace("&", string.Empty);
            text = text.Replace("'", string.Empty);
            text = regex.Replace(text, "-");
            text = RemoveCharacters(text);

            return text;
        }

        /// <summary>
        /// Remover caracteres especiales
        /// </summary>
        /// <param name="text">texto a analizar</param>
        /// <returns></returns>
        private static string RemoveCharacters(string text)
        {
            String normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (int i = 0; i < normalized.Length; i++)
            {
                Char c = normalized[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return HttpUtility.UrlEncode(sb.ToString()).Replace("%", string.Empty);
        }

        /// <summary>
        /// Retira todas las etiquetas html del string especificado
        /// </summary>
        /// <param name="html">el estring que cntiene el html</param>
        /// <returns>un string sin etiquetas html</returns>
        public static string StripHtml(string html)
        {
            var strip_HTML = new Regex(@"<(.|\n)*?>", RegexOptions.Compiled);
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return strip_HTML.Replace(html, string.Empty);
        }

        public static DateTime ConvertirFecha(string valor)
        {
            var fechaArray = valor.Split('/');
            var fecha = new DateTime(Convert.ToInt32(fechaArray[2].Substring(0, 4)), Convert.ToInt32(fechaArray[1]), Convert.ToInt32(fechaArray[0]));
            return fecha;
        }

        #endregion Formatear datos

        #region Manejo de URLs

        private static string relativeWebRoot;

        /// <summary>
        /// Retorna la ruta relativa al sitio
        /// </summary>
        public static string RelativeWebRoot
        {
            get
            {
                if (relativeWebRoot == null)
                    relativeWebRoot = VirtualPathUtility.ToAbsolute("~/");
                return relativeWebRoot;
            }
        }

        /// <summary>
        /// Retorna la ruta absoluta al sitio
        /// </summary>
        public static Uri AbsoluteWebRoot
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null)
                    throw new WebException("El actual HttpContext es nulo");

                if (context.Items["absoluteurl"] == null)
                    context.Items["absoluteurl"] =
                        new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot);

                return context.Items["absoluteurl"] as Uri;
            }
        }

        #endregion Manejo de URLs

        #region Emails

        public static bool SendEmail(string email, string asunto, string mensaje)
        {
            var message = new MailMessage
            {
                BodyEncoding = Encoding.UTF8,
                Subject = asunto,
                SubjectEncoding = Encoding.UTF8,
                Body = mensaje,
                IsBodyHtml = true,
                Priority = MailPriority.Normal
            };
            message.To.Add(email);

            try
            {
                var smtp = new SmtpClient { EnableSsl = true };
                smtp.Send(message);
                return true;
            }
            catch (SmtpException)
            {
                return false;
            }
            finally
            {
                message.Dispose();
            }
        }

        #endregion Emails

        #region Manejo de datos

        public static List<Comun> EnumToList<T>()
        {
            var enumType = typeof(T);

            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T debe ser de tipo System.Enum");

            var enumValArray = Enum.GetValues(enumType);
            var enumValList = (from object l in enumValArray
                               select new Comun
                               {
                                   Id = Convert.ToInt32(l),
                                   Nombre = Enum.GetName(enumType, l)
                               })
                .OrderBy(p => p.Nombre)
                .ToList();
            return enumValList;
        }

        public static List<Comun> ConvertToComunList<T>(IList<T> list, string value, string text)
        {
            var listItems = (from entity in list
                             let propiedad1 = entity.GetType().GetProperty(value)
                             where propiedad1 != null
                             let valor1 = propiedad1.GetValue(entity, null)
                             where valor1 != null
                             let propiedad2 = entity.GetType().GetProperty(text)
                             where propiedad2 != null
                             let valor2 = propiedad2.GetValue(entity, null)
                             where valor2 != null
                             select new Comun
                             {
                                 Id = Convert.ToInt32(valor1),
                                 Nombre = valor2.ToString()
                             })
                .OrderBy(p => p.Nombre)
                .ToList();
            listItems.Insert(0, new Comun { Nombre = "-- Seleccionar --", Id = 0 });
            return listItems;
        }

        #endregion Manejo de datos

        #region Archivos

        public static void EliminarArchivo(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void VerificarExistenciaArchivo(string path)
        {
            if (!File.Exists(path))
            {
                using (var sw = new StreamWriter(path, true))
                {
                    sw.Dispose();
                }
            }
        }

        public static string ObtenerGetType(string nombreArchivo)
        {
            #region DiccionarioContentType
            var contentTypes = new Dictionary<string, string>
                               {
                                   {"3dm", "x-world/x-3dmf"},
                                   {"3dmf", "x-world/x-3dmf"},
                                   {"a", "application/octet-stream"},
                                   {"aab", "application/x-authorware-bin"},
                                   {"aam", "application/x-authorware-map"},
                                   {"aas", "application/x-authorware-seg"},
                                   {"abc", "text/vnd.abc"},
                                   {"acgi", "text/html"},
                                   {"afl", "video/animaflex"},
                                   {"ai", "application/postscript"},
                                   {"aif", "audio/aiff"},
                                   {"aifc", "audio/aiff"},
                                   {"aiff", "audio/aiff"},
                                   {"aim", "application/x-aim"},
                                   {"aip", "text/x-audiosoft-intra"},
                                   {"ani", "application/x-navi-animation"},
                                   {"aos", "application/x-nokia-9000-communicator-add-on-software"},
                                   {"aps", "application/mime"},
                                   {"arc", "application/octet-stream"},
                                   {"arj", "application/arj"},
                                   {"art", "image/x-jg"},
                                   {"asf", "video/x-ms-asf"},
                                   {"asm", "text/x-asm"},
                                   {"asp", "text/asp"},
                                   {"asx", "application/x-mplayer2"},
                                   {"au", "audio/basic"},
                                   {"avi", "video/avi"},
                                   {"avs", "video/avs-video"},
                                   {"bcpio", "application/x-bcpio"},
                                   {"bin", "application/octet-stream"},
                                   {"bm", "image/bmp"},
                                   {"bmp", "image/bmp"},
                                   {"boo", "application/book"},
                                   {"book", "application/book"},
                                   {"boz", "application/x-bzip2"},
                                   {"bsh", "application/x-bsh"},
                                   {"bz", "application/x-bzip"},
                                   {"bz2", "application/x-bzip2"},
                                   {"c", "text/plain"},
                                   {"c++", "text/plain"},
                                   {"cat", "application/vnd.ms-pki.seccat"},
                                   {"cc", "text/plain"},
                                   {"ccad", "application/clariscad"},
                                   {"cco", "application/x-cocoa"},
                                   {"cdf", "application/cdf"},
                                   {"cer", "application/pkix-cert"},
                                   {"cha", "application/x-chat"},
                                   {"chat", "application/x-chat"},
                                   {"class", "application/java"},
                                   {"com", "application/octet-stream"},
                                   {"conf", "text/plain"},
                                   {"cpio", "application/x-cpio"},
                                   {"cpp", "text/x-c"},
                                   {"cpt", "application/x-cpt"},
                                   {"crl", "application/pkcs-crl"},
                                   {"css", "text/css"},
                                   {"def", "text/plain"},
                                   {"der", "application/x-x509-ca-cert"},
                                   {"dif", "video/x-dv"},
                                   {"dir", "application/x-director"},
                                   {"dl", "video/dl"},
                                   {"doc", "application/msword"},
                                   {"dot", "application/msword"},
                                   {"dp", "application/commonground"},
                                   {"drw", "application/drafting"},
                                   {"dump", "application/octet-stream"},
                                   {"dv", "video/x-dv"},
                                   {"dvi", "application/x-dvi"},
                                   {"dwf", "drawing/x-dwf (old)"},
                                   {"dwg", "application/acad"},
                                   {"dxf", "application/dxf"},
                                   {"eps", "application/postscript"},
                                   {"es", "application/x-esrehber"},
                                   {"etx", "text/x-setext"},
                                   {"evy", "application/envoy"},
                                   {"exe", "application/octet-stream"},
                                   {"f", "text/plain"},
                                   {"f90", "text/x-fortran"},
                                   {"fdf", "application/vnd.fdf"},
                                   {"fif", "image/fif"},
                                   {"fli", "video/fli"},
                                   {"for", "text/x-fortran"},
                                   {"fpx", "image/vnd.fpx"},
                                   {"g", "text/plain"},
                                   {"g3", "image/g3fax"},
                                   {"gif", "image/gif"},
                                   {"gl", "video/gl"},
                                   {"gsd", "audio/x-gsm"},
                                   {"gtar", "application/x-gtar"},
                                   {"gz", "application/x-compressed"},
                                   {"h", "text/plain"},
                                   {"help", "application/x-helpfile"},
                                   {"hgl", "application/vnd.hp-hpgl"},
                                   {"hh", "text/plain"},
                                   {"hlp", "application/x-winhelp"},
                                   {"htc", "text/x-component"},
                                   {"htm", "text/html"},
                                   {"html", "text/html"},
                                   {"htmls", "text/html"},
                                   {"htt", "text/webviewhtml"},
                                   {"htx", "text/html"},
                                   {"ice", "x-conference/x-cooltalk"},
                                   {"ico", "image/x-icon"},
                                   {"idc", "text/plain"},
                                   {"ief", "image/ief"},
                                   {"iefs", "image/ief"},
                                   {"iges", "application/iges"},
                                   {"igs", "application/iges"},
                                   {"ima", "application/x-ima"},
                                   {"imap", "application/x-httpd-imap"},
                                   {"inf", "application/inf"},
                                   {"ins", "application/x-internett-signup"},
                                   {"ip", "application/x-ip2"},
                                   {"isu", "video/x-isvideo"},
                                   {"it", "audio/it"},
                                   {"iv", "application/x-inventor"},
                                   {"ivr", "i-world/i-vrml"},
                                   {"ivy", "application/x-livescreen"},
                                   {"jam", "audio/x-jam"},
                                   {"jav", "text/plain"},
                                   {"java", "text/plain"},
                                   {"jcm", "application/x-java-commerce"},
                                   {"jfif", "image/jpeg"},
                                   {"jfif-tbnl", "image/jpeg"},
                                   {"jpe", "image/jpeg"},
                                   {"jpeg", "image/jpeg"},
                                   {"jpg", "image/jpeg"},
                                   {"jps", "image/x-jps"},
                                   {"js", "application/x-javascript"},
                                   {"jut", "image/jutvision"},
                                   {"kar", "audio/midi"},
                                   {"ksh", "application/x-ksh"},
                                   {"la", "audio/nspaudio"},
                                   {"lam", "audio/x-liveaudio"},
                                   {"latex", "application/x-latex"},
                                   {"lha", "application/lha"},
                                   {"lhx", "application/octet-stream"},
                                   {"list", "text/plain"},
                                   {"lma", "audio/nspaudio"},
                                   {"log", "text/plain"},
                                   {"lsp", "application/x-lisp"},
                                   {"lst", "text/plain"},
                                   {"lsx", "text/x-la-asf"},
                                   {"ltx", "application/x-latex"},
                                   {"lzh", "application/octet-stream"},
                                   {"lzx", "application/lzx"},
                                   {"m", "text/plain"},
                                   {"m1v", "video/mpeg"},
                                   {"m2a", "audio/mpeg"},
                                   {"m2v", "video/mpeg"},
                                   {"m3u", "audio/x-mpequrl"},
                                   {"man", "application/x-troff-man"},
                                   {"map", "application/x-navimap"},
                                   {"mar", "text/plain"},
                                   {"mbd", "application/mbedlet"},
                                   {"mc$", "application/x-magic-cap-package-1.0"},
                                   {"mcd", "application/mcad"},
                                   {"mcf", "image/vasa"},
                                   {"mcp", "application/netmc"},
                                   {"me", "application/x-troff-me"},
                                   {"mht", "message/rfc822"},
                                   {"mhtml", "message/rfc822"},
                                   {"mid", "audio/midi"},
                                   {"midi", "audio/midi"},
                                   {"mif", "application/x-frame"},
                                   {"mime", "message/rfc822"},
                                   {"mjf", "audio/x-vnd.audioexplosion.mjuicemediafile"},
                                   {"mjpg", "video/x-motion-jpeg"},
                                   {"mm", "application/base64"},
                                   {"mme", "application/base64"},
                                   {"mod", "audio/mod"},
                                   {"moov", "video/quicktime"},
                                   {"mov", "video/quicktime"},
                                   {"movie", "video/x-sgi-movie"},
                                   {"mp2", "audio/mpeg"},
                                   {"mp3", "audio/mpeg3"},
                                   {"mpa", "audio/mpeg"},
                                   {"mpc", "application/x-project"},
                                   {"mpe", "video/mpeg"},
                                   {"mpeg", "video/mpeg"},
                                   {"mpg", "video/mpeg"},
                                   {"mpga", "audio/mpeg"},
                                   {"mpp", "application/vnd.ms-project"},
                                   {"mpt", "application/x-project"},
                                   {"mpv", "application/x-project"},
                                   {"mpx", "application/x-project"},
                                   {"mrc", "application/marc"},
                                   {"ms", "application/x-troff-ms"},
                                   {"mv", "video/x-sgi-movie"},
                                   {"my", "audio/make"},
                                   {"mzz", "application/x-vnd.audioexplosion.mzz"},
                                   {"nap", "image/naplps"},
                                   {"naplps", "image/naplps"},
                                   {"nc", "application/x-netcdf"},
                                   {"ncm", "application/vnd.nokia.configuration-message"},
                                   {"nif", "image/x-niff"},
                                   {"niff", "image/x-niff"},
                                   {"nix", "application/x-mix-transfer"},
                                   {"nsc", "application/x-conference"},
                                   {"nvd", "application/x-navidoc"},
                                   {"o", "application/octet-stream"},
                                   {"oda", "application/oda"},
                                   {"omc", "application/x-omc"},
                                   {"omcd", "application/x-omcdatamaker"},
                                   {"omcr", "application/x-omcregerator"},
                                   {"p", "text/x-pascal"},
                                   {"p10", "application/pkcs10"},
                                   {"p12", "application/pkcs-12"},
                                   {"p7a", "application/x-pkcs7-signature"},
                                   {"p7c", "application/pkcs7-mime"},
                                   {"pas", "text/pascal"},
                                   {"pbm", "image/x-portable-bitmap"},
                                   {"pcl", "application/vnd.hp-pcl"},
                                   {"pct", "image/x-pict"},
                                   {"pcx", "image/x-pcx"},
                                   {"pdf", "application/pdf"},
                                   {"pfunk", "audio/make"},
                                   {"pgm", "image/x-portable-graymap"},
                                   {"pic", "image/pict"},
                                   {"pict", "image/pict"},
                                   {"pkg", "application/x-newton-compatible-pkg"},
                                   {"pko", "application/vnd.ms-pki.pko"},
                                   {"pl", "text/plain"},
                                   {"plx", "application/x-pixclscript"},
                                   {"pm", "image/x-xpixmap"},
                                   {"png", "image/png"},
                                   {"pnm", "application/x-portable-anymap"},
                                   {"pot", "application/mspowerpoint"},
                                   {"pov", "model/x-pov"},
                                   {"ppa", "application/vnd.ms-powerpoint"},
                                   {"ppm", "image/x-portable-pixmap"},
                                   {"pps", "application/mspowerpoint"},
                                   {"ppt", "application/mspowerpoint"},
                                   {"ppz", "application/mspowerpoint"},
                                   {"pre", "application/x-freelance"},
                                   {"prt", "application/pro_eng"},
                                   {"ps", "application/postscript"},
                                   {"psd", "application/octet-stream"},
                                   {"pvu", "paleovu/x-pv"},
                                   {"pwz", "application/vnd.ms-powerpoint"},
                                   {"py", "text/x-script.phyton"},
                                   {"pyc", "applicaiton/x-bytecode.python"},
                                   {"qcp", "audio/vnd.qcelp"},
                                   {"qd3", "x-world/x-3dmf"},
                                   {"qd3d", "x-world/x-3dmf"},
                                   {"qif", "image/x-quicktime"},
                                   {"qt", "video/quicktime"},
                                   {"qtc", "video/x-qtc"},
                                   {"qti", "image/x-quicktime"},
                                   {"qtif", "image/x-quicktime"},
                                   {"ra", "audio/x-pn-realaudio"},
                                   {"ram", "audio/x-pn-realaudio"},
                                   {"ras", "application/x-cmu-raster"},
                                   {"rast", "image/cmu-raster"},
                                   {"rexx", "text/x-script.rexx"},
                                   {"rf", "image/vnd.rn-realflash"},
                                   {"rgb", "image/x-rgb"},
                                   {"rm", "application/vnd.rn-realmedia"},
                                   {"rmi", "audio/mid"},
                                   {"rmm", "audio/x-pn-realaudio"},
                                   {"rmp", "audio/x-pn-realaudio"},
                                   {"rng", "application/ringing-tones"},
                                   {"rnx", "application/vnd.rn-realplayer"},
                                   {"roff", "application/x-troff"},
                                   {"rp", "image/vnd.rn-realpix"},
                                   {"rpm", "audio/x-pn-realaudio-plugin"},
                                   {"rt", "text/richtext"},
                                   {"rtf", "text/richtext"},
                                   {"rtx", "application/rtf"},
                                   {"rv", "video/vnd.rn-realvideo"},
                                   {"s", "text/x-asm"},
                                   {"s3m", "audio/s3m"},
                                   {"saveme", "application/octet-stream"},
                                   {"sbk", "application/x-tbook"},
                                   {"scm", "application/x-lotusscreencam"},
                                   {"sdml", "text/plain"},
                                   {"sdp", "application/sdp"},
                                   {"sdr", "application/sounder"},
                                   {"sea", "application/sea"},
                                   {"set", "application/set"},
                                   {"sgm", "text/sgml"},
                                   {"sgml", "text/sgml"},
                                   {"sh", "application/x-bsh"},
                                   {"shtml", "text/html"},
                                   {"sid", "audio/x-psid"},
                                   {"sit", "application/x-sit"},
                                   {"skd", "application/x-koan"},
                                   {"skm", "application/x-koan"},
                                   {"skp", "application/x-koan"},
                                   {"skt", "application/x-koan"},
                                   {"sl", "application/x-seelogo"},
                                   {"smi", "application/smil"},
                                   {"smil", "application/smil"},
                                   {"snd", "audio/basic"},
                                   {"sol", "application/solids"},
                                   {"spc", "application/x-pkcs7-certificates"},
                                   {"spl", "application/futuresplash"},
                                   {"spr", "application/x-sprite"},
                                   {"sprite", "application/x-sprite"},
                                   {"src", "application/x-wais-source"},
                                   {"ssi", "text/x-server-parsed-html"},
                                   {"ssm", "application/streamingmedia"},
                                   {"sst", "application/vnd.ms-pki.certstore"},
                                   {"step", "application/step"},
                                   {"stl", "application/sla"},
                                   {"stp", "application/step"},
                                   {"sv4cpio", "application/x-sv4cpio"},
                                   {"sv4crc", "application/x-sv4crc"},
                                   {"svf", "image/vnd.dwg"},
                                   {"svr", "application/x-world"},
                                   {"swf", "application/x-shockwave-flash"},
                                   {"t", "application/x-troff"},
                                   {"talk", "text/x-speech"},
                                   {"tar", "application/x-tar"},
                                   {"tbk", "application/toolbook"},
                                   {"tcl", "application/x-tcl"},
                                   {"tcsh", "text/x-script.tcsh"},
                                   {"tex", "application/x-tex"},
                                   {"texi", "application/x-texinfo"},
                                   {"texinfo", "application/x-texinfo"},
                                   {"text", "text/plain"},
                                   {"tgz", "application/x-compressed"},
                                   {"tif", "image/tiff"},
                                   {"tr", "application/x-troff"},
                                   {"tsi", "audio/tsp-audio"},
                                   {"tsp", "audio/tsplayer"},
                                   {"tsv", "text/tab-separated-values"},
                                   {"turbot", "image/florian"},
                                   {"txt", "text/plain"},
                                   {"uil", "text/x-uil"},
                                   {"uni", "text/uri-list"},
                                   {"unis", "text/uri-list"},
                                   {"unv", "application/i-deas"},
                                   {"uri", "text/uri-list"},
                                   {"uris", "text/uri-list"},
                                   {"ustar", "application/x-ustar"},
                                   {"uu", "application/octet-stream"},
                                   {"vcd", "application/x-cdlink"},
                                   {"vcs", "text/x-vcalendar"},
                                   {"vda", "application/vda"},
                                   {"vdo", "video/vdo"},
                                   {"vew", "application/groupwise"},
                                   {"viv", "video/vivo"},
                                   {"vivo", "video/vivo"},
                                   {"vmd", "application/vocaltec-media-desc"},
                                   {"vmf", "application/vocaltec-media-file"},
                                   {"voc", "audio/voc"},
                                   {"vos", "video/vosaic"},
                                   {"vox", "audio/voxware"},
                                   {"vqe", "audio/x-twinvq-plugin"},
                                   {"vqf", "audio/x-twinvq"},
                                   {"vql", "audio/x-twinvq-plugin"},
                                   {"vrml", "application/x-vrml"},
                                   {"vrt", "x-world/x-vrt"},
                                   {"vsd", "application/x-visio"},
                                   {"vst", "application/x-visio"},
                                   {"vsw", "application/x-visio"},
                                   {"w60", "application/wordperfect6.0"},
                                   {"w61", "application/wordperfect6.1"},
                                   {"w6w", "application/msword"},
                                   {"wav", "audio/wav"},
                                   {"wb1", "application/x-qpro"},
                                   {"wbmp", "image/vnd.wap.wbmp"},
                                   {"web", "application/vnd.xara"},
                                   {"wiz", "application/msword"},
                                   {"wk1", "application/x-123"},
                                   {"wmf", "windows/metafile"},
                                   {"wml", "text/vnd.wap.wml"},
                                   {"wmlc", "application/vnd.wap.wmlc"},
                                   {"wmls", "text/vnd.wap.wmlscript"},
                                   {"wmlsc", "application/vnd.wap.wmlscriptc"},
                                   {"word", "application/msword"},
                                   {"wp", "application/wordperfect"},
                                   {"wp5", "application/wordperfect"},
                                   {"wp6", "application/wordperfect"},
                                   {"wpd", "application/wordperfect"},
                                   {"wq1", "application/x-lotus"},
                                   {"wri", "application/mswrite"},
                                   {"wrl", "application/x-world"},
                                   {"wrz", "model/vrml"},
                                   {"wsc", "text/scriplet"},
                                   {"wsrc", "application/x-wais-source"},
                                   {"wtk", "application/x-wintalk"},
                                   {"xbm", "image/x-xbitmap"},
                                   {"xdr", "video/x-amt-demorun"},
                                   {"xgz", "xgl/drawing"},
                                   {"xif", "image/vnd.xiff"},
                                   {"xl", "application/excel"},
                                   {"xla", "application/excel"},
                                   {"xlb", "application/excel"},
                                   {"xlc", "application/excel"},
                                   {"xld", "application/excel"},
                                   {"xlk", "application/excel"},
                                   {"xll", "application/excel"},
                                   {"xlm", "application/excel"},
                                   {"xls", "application/excel"},
                                   {"xlt", "application/excel"},
                                   {"xlv", "application/excel"},
                                   {"xlw", "application/excel"},
                                   {"xm", "audio/xm"},
                                   {"xml", "text/xml"},
                                   {"xmz", "xgl/movie"},
                                   {"xpix", "application/x-vnd.ls-xpix"},
                                   {"xpm", "image/x-xpixmap"},
                                   {"x-png", "image/png"},
                                   {"xsr", "video/x-amt-showrun"},
                                   {"xwd", "image/x-xwd"},
                                   {"xyz", "chemical/x-pdb"},
                                   {"z", "application/x-compress"},
                                   {"zip", "application/x-compressed"},
                                   {"zoo", "application/octet-stream"},
                                   {"zsh", "text/x-script.zsh"}
                               };
            #endregion DiccionarioContentType
            var extension = Path.GetExtension(nombreArchivo).ToLower().TrimStart('.');
                        
            string contentType;
            
            contentTypes.TryGetValue(extension.ToLower(), out contentType);

            if (String.IsNullOrEmpty(contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        #endregion Archivos


        /// <summary>
        /// Encripta la clave del usuario
        /// </summary>
        /// <param name="textoNormal"></param>
        /// <returns>cadena encriptada</returns>
        public static string Encriptar(string textoNormal)
        {
            string claveEncriptada = FormsAuthentication.HashPasswordForStoringInConfigFile(textoNormal, "md5");
            return claveEncriptada;
        }
    }
}