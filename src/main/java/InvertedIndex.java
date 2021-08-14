import java.io.File;
import java.util.*;

public class InvertedIndex {

    private static final List<String> stopWords;

    static {
        stopWords = Arrays.asList(FileReader.readFileContent(new File(FilePaths.STOP_WORDS.filePath)).split(","));
    }

    private Map<String, List<WordInfo>> index = new HashMap<>();

    public void indexAllFiles(String folderAddress) {
        File folder = new File(folderAddress);
        File[] listOfFiles = folder.listFiles();

        System.out.println("indexing...");
        for (File listOfFile : listOfFiles) {
            indexFile(new File(folderAddress + "/" + listOfFile.getName()));
        }
        System.out.println("indexing completed.");
    }

    private void indexFile(File file) {
        String fileName = file.getName();

        int position = 0;
        String text = FileReader.readFileContent(file);
        for (String word : text.split("\\W+")) {
            word = word.toLowerCase();
            position++;
            if (stopWords.contains(word))
                continue;
            List<WordInfo> idx = index.computeIfAbsent(word, k -> new LinkedList<>());
            idx.add(new WordInfo(fileName, position));
        }
    }

    public static List<String> getStopWords() {
        return stopWords;
    }

    public Map<String, List<WordInfo>> getIndex() {
        return index;
    }

}