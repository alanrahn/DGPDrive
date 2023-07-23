namespace ApiUtil
{
    public class ResultHTML
    {
        public ResultHTML()
        {

        }

        public string HTMLStart()
        {
            return "<!DOCTYPE html><html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">" + 
                    "<head><meta charset=\"utf-8\" /><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />" + 
                    "<style type=\"text/css\">" +
                    "body {margin: 0; padding: 0; background-color: #404040; font-family:Verdana; font-size:x-small; color:lightgray;}" +
                    ".titlediv {color:cornflowerblue; margin: 30px 30px 10px 30px; font-size: 110%;}" +
                    ".innerdiv {margin: 10px 30px 10px 30px; font-size: 90%;}" +
                    ".innerdiv p {margin: 10px 10px 10px 10px; font-size: xsmall;}" +
                    ".success {color: cadetblue;}" +
                    ".error {color: sienna;}" +
                    "table, th, td {border: 1px solid black; padding: 5px 10px 5px 10px;}" +
                    "table {border-collapse: collapse; width: 100 %;}" +
                    "td {height: 20px;}" +
                    "tr {background-color: #424242; color: lightgray;}" +
                    "th {background-color: dimgray; color: black;}" +
                    "</style>" + 
                    "<title>Results</title></head><body>";
        }

        public string HTMLEnd()
        {
            return "</body></html>";
        }
    }
}
