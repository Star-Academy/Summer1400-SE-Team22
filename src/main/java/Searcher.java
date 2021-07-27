import java.util.*;

public class Searcher {

    private static InvertedIndex invertedIndex;

    public static void run(String folderAddress) {
        try {
            invertedIndex = new InvertedIndex();
            invertedIndex.indexAllFiles(folderAddress);
            Scanner scanner = new Scanner(System.in);
            while (true) {
                System.out.println("enter a word for search:");
                search(scanner.nextLine());
                System.out.println("---------------------------------------------------");
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static void search(String searchingExpression) {

        HashMap<String, WordInfo> allCandidates = new HashMap<>();
        searchingExpression = searchingExpression.toLowerCase();
        List<String> words = new LinkedList<>(Arrays.asList(searchingExpression.split("\\s+")));
        List<String> plusWords = new LinkedList<>();
        List<String> minusWords = new LinkedList<>();

        isolatePlusAndMinusWords(words, plusWords, minusWords);

        int navigatingIndex = 0;
        try {
            while (InvertedIndex.getStopWords().contains(words.get(navigatingIndex))) {
                navigatingIndex++;
            }
        } catch (Exception e) {
            System.out.println(ConsoleColors.ANSI_RED + "please try a different keyword for your search!"
                    + ConsoleColors.ANSI_RESET);
            return;
        }
        List<WordInfo> candidates;
        try {
            candidates = new LinkedList<>(searchForAWord(words.get(navigatingIndex)));
        } catch (Exception e){
            return;
        }
        int ignoredCounter = 0;
        for (navigatingIndex += 1; navigatingIndex < words.size(); navigatingIndex++) {
            String word = words.get(navigatingIndex);
            if (InvertedIndex.getStopWords().contains(word)) {
                ignoredCounter++;
                continue;
            }

            List<WordInfo> demo = searchForAWord(words.get(navigatingIndex));
            reduceResultsToMatchSearch(candidates, ignoredCounter, demo);

            handlePlusWords(allCandidates, plusWords);

            ignoredCounter = 0;
        }

        sumResultsWithPlusWords(allCandidates, candidates);

        candidates = new LinkedList<>(allCandidates.values());

        deleteMinusWordsFromCandidates(minusWords, candidates);

        printResults(words, candidates);
    }

    private static void reduceResultsToMatchSearch(List<WordInfo> candidates, int ignoredCounter, List<WordInfo> demo) {
        for (int j = candidates.size() - 1; j >= 0; j--) {
            WordInfo candidate = candidates.get(j);
            boolean isExist = false;

            for (WordInfo wordInfo : demo) {
                if (wordInfo.getFileName().equals(candidate.getFileName())
                        && candidate.getPosition() + ignoredCounter + 1 == wordInfo.getPosition()) {
                    candidates.set(j, wordInfo);
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
                candidates.remove(j);
        }
    }

    private static void sumResultsWithPlusWords(HashMap<String, WordInfo> allCandidates, List<WordInfo> candidates) {
        for (WordInfo candidate : candidates) {
            allCandidates.put(candidate.getFileName(), candidate);
        }
    }

    private static void handlePlusWords(HashMap<String, WordInfo> allCandidates, List<String> plusWords) {
        for (String plusWord : plusWords) {
            for (WordInfo wordInfo : searchForAWord(plusWord)) {
                allCandidates.put(wordInfo.getFileName(), wordInfo);
            }
        }
    }

    private static void printResults(List<String> words, List<WordInfo> candidates) {
        for (WordInfo candidate : candidates) {
            System.out.println("File name: " + ConsoleColors.ANSI_CYAN + candidate.getFileName() + ConsoleColors.ANSI_RESET
                    + " Position: " + ConsoleColors.ANSI_GREEN + (candidate.getPosition() - words.size() + 1)
                    + ConsoleColors.ANSI_RESET);
        }
    }

    private static void isolatePlusAndMinusWords(List<String> words, List<String> plusWords, List<String> minusWords) {
        for (int i = words.size() - 1; i >= 0; i--) {
            String word = words.get(i);
            if (word.startsWith("+")) {
                plusWords.add(word.substring(1));
                words.remove(i);
            } else if (word.startsWith("-")) {
                minusWords.add(word.substring(1));
                words.remove(i);
            }
        }
    }

    private static List<WordInfo> searchForAWord(String word) {
        word = word.toLowerCase();
        return invertedIndex.getIndex().get(word);
    }

    private static void deleteMinusWordsFromCandidates(List<String> minusWords, List<WordInfo> candidates) {
        for (String minusWord : minusWords) {
            try {
                List<WordInfo> toBeRemovedDocs = searchForAWord(minusWord);
                for (WordInfo toBeRemovedDoc : toBeRemovedDocs) {
                    for (int j = candidates.size() - 1; j >= 0; j--) {
                        if (candidates.get(j).getFileName().equals(toBeRemovedDoc.getFileName()))
                            candidates.remove(j);
                    }
                }
            } catch (Exception ignored) {
            }
        }
    }

    public static void setInvertedIndex(InvertedIndex invertedIndex) {
        Searcher.invertedIndex = invertedIndex;
    }
}