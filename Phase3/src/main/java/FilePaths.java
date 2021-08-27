public enum FilePaths {
    STOP_WORDS("src/main/resources/stopWords.txt"),
    DATABASE("src/main/resources/SampleEnglishData/EnglishData");

    public final String filePath;

    FilePaths(String filePath) {
        this.filePath = filePath;
    }
}
