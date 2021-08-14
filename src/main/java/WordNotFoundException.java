public class WordNotFoundException extends Exception{
    public WordNotFoundException() {
        super(ConsoleColors.ANSI_RED + "please try a different keyword for your search!" + ConsoleColors.ANSI_RESET);
    }
}
