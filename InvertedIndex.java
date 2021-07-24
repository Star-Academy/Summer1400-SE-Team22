import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.*;

public class InvertedIndex {

    List<String> stopWords = Arrays.asList("a", "able", "about",
            "across", "after", "all", "almost", "also", "am", "among", "an",
            "and", "any", "are", "as", "at", "be", "because", "been", "but",
            "by", "can", "cannot", "could", "dear", "did", "do", "does",
            "either", "else", "ever", "every", "for", "from", "get", "got",
            "had", "has", "have", "he", "her", "hers", "him", "his", "how",
            "however", "i", "if", "in", "into", "is", "it", "its", "just",
            "least", "let", "like", "likely", "may", "me", "might", "most",
            "must", "my", "neither", "no", "nor", "not", "of", "off", "often",
            "on", "only", "or", "other", "our", "own", "rather", "said", "say",
            "says", "she", "should", "since", "so", "some", "than", "that",
            "the", "their", "them", "then", "there", "these", "they", "this",
            "tis", "to", "too", "twas", "us", "wants", "was", "we", "were",
            "what", "when", "where", "which", "while", "who", "whom", "why",
            "will", "with", "would", "yet", "you", "your");

    Map<String, List<WordInfo>> index = new HashMap<>();
    List<String> files = new ArrayList<>();

    public static void run() {
        try {
            String folderAddress = "SampleEnglishData/EnglishData";
            File folder = new File(folderAddress);
            File[] listOfFiles = folder.listFiles();

            InvertedIndex idx = new InvertedIndex();
            System.out.println("indexing...");
            for (File listOfFile : listOfFiles) {
                idx.indexFile(new File(folderAddress + "/" + listOfFile.getName()));
            }
            System.out.println("indexing completed.");
            Scanner scanner = new Scanner(System.in);
            System.out.println("enter searching word:");
            idx.search(Arrays.asList(scanner.nextLine().split(",")));
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void indexFile(File file) throws IOException {
        String fileName = file.getName();

        int position = 0;
        BufferedReader reader = new BufferedReader(new FileReader(file));
        for (String line = reader.readLine(); line != null; line = reader.readLine()) {
            for (String word : line.split("\\W+")) {
                word = word.toLowerCase();
                position++;
                if (stopWords.contains(word))
                    continue;
                List<WordInfo> idx = index.computeIfAbsent(word, k -> new LinkedList<>());
                idx.add(new WordInfo(fileName, position));
            }
        }
    }

    public void search(List<String> words) {
        for (String _word : words) {
            Set<String> answer = new HashSet<>();
            String word = _word.toLowerCase();
            List<WordInfo> idx = index.get(word);
            if (idx != null) {
                for (WordInfo t : idx) {
                    answer.add(files.get(t.fileName));
                }
            }
            System.out.print(word);
            for (String f : answer) {
                System.out.print(" " + f);
            }
            System.out.println("");
        }
    }

}