import java.io.File;
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
    String folderAddress;

    public InvertedIndex(String folderAddress) {
        this.folderAddress = folderAddress;
    }

    public static void run(String folderAddress) {
        try {
            File folder = new File(folderAddress);
            File[] listOfFiles = folder.listFiles();

            InvertedIndex idx = new InvertedIndex(folderAddress);
            System.out.println("indexing...");
            for (File listOfFile : listOfFiles) {
                idx.indexFile(new File(folderAddress + "/" + listOfFile.getName()));
            }
            System.out.println("indexing completed.");
            Scanner scanner = new Scanner(System.in);
            while (true) {
                System.out.println("enter a word for search:");
                idx.search(scanner.nextLine());
                System.out.println("---------------------------------------------------");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void indexFile(File file) {
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

    public void search(String searchingExpression) {
        String[] words = searchingExpression.split("\\W+");

        int firstNonStopWord = 0;
        while (stopWords.contains(words[firstNonStopWord])) {
            firstNonStopWord++;
        }

        List<WordInfo> candidates = new LinkedList<>(searchForAWord(words[firstNonStopWord]));

        int ignoredCounter = 0;
        for (int i = firstNonStopWord + 1 ; i < words.length; i++) {
            String word = words[i];
            if (stopWords.contains(word)) {
                ignoredCounter++;
                continue;
            }

            List<WordInfo> demo = searchForAWord(words[i]);
            for (int j = candidates.size() - 1; j >= 0; j--) {
                WordInfo candidate = candidates.get(j);
                WordInfo candidateInDemo = null;
                boolean isExist = false;

                for (WordInfo wordInfo : demo) {
                    if (wordInfo.fileName.equals(candidate.fileName)) {
                        candidateInDemo = wordInfo;
                        isExist = true;
                        break;
                    }
                }
                if (!isExist || candidate.position + ignoredCounter + 1 != candidateInDemo.position)
                    candidates.remove(candidate);
            }

            ignoredCounter = 0;
        }
        for (WordInfo candidate : candidates) {
            System.out.println("File name :" + candidate.fileName + " Position :" + candidate.position);
        }

    }

    private List<WordInfo> searchForAWord(String word) {
        word = word.toLowerCase();
        return index.get(word);
    }

}