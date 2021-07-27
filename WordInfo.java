public class WordInfo {
    private String fileName;
    private int position;

    public WordInfo(String fileName, int position) {
        this.fileName = fileName;
        this.position = position;
    }

    public String getFileName() {
        return fileName;
    }

    public int getPosition() {
        return position;
    }
}
