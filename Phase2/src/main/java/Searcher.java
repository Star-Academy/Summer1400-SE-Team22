import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Scanner;

public class Searcher {

    private InvertedIndex invertedIndex;
    private SearchingExpression searchingExpression;

    public void run(String folderAddress) {
        invertedIndex = new InvertedIndex();
        invertedIndex.indexAllFiles(folderAddress);
        Scanner scanner = new Scanner(System.in);
        while (true) {
            System.out.println("enter a word for search or EXIT to exit:");
            String input = scanner.nextLine();
            if (input.equals("EXIT")) return;
            searchingExpression = new SearchingExpression(input);
            printResults(search());
            System.out.println("---------------------------------------------------");
        }
    }

    public List<WordInfo> search() {
        List<WordInfo> candidates;

        int navigatingIndex = 0;
        try {
            while (InvertedIndex.getStopWords().contains(searchingExpression.getWords().get(navigatingIndex)))
                navigatingIndex++;
            candidates = new LinkedList<>(searchForAWord(searchingExpression.getWords().get(navigatingIndex)));
        } catch (Exception e) {
            return null;
        }
        int ignoredCounter = 0;
        for (navigatingIndex += 1; navigatingIndex < searchingExpression.getWords().size(); navigatingIndex++) {
            String word = searchingExpression.getWords().get(navigatingIndex);
            if (InvertedIndex.getStopWords().contains(word)) {
                ignoredCounter++;
                continue;
            }
            List<WordInfo> demo = searchForAWord(searchingExpression.getWords().get(navigatingIndex));
            reduceResultsToMatchSearch(candidates, ignoredCounter, demo);
            ignoredCounter = 0;
        }
        candidatesProcessor(candidates);
        return candidates;
    }

    private void candidatesProcessor(List<WordInfo> candidates) {
        HashMap<String, WordInfo> allCandidates = new HashMap<>();
        handlePlusWords(allCandidates);
        sumResultsWithPlusWords(allCandidates, candidates);
        candidates = new LinkedList<>(allCandidates.values());
        deleteMinusWordsFromCandidates(candidates);
    }

    private void reduceResultsToMatchSearch(List<WordInfo> candidates, int ignoredCounter, List<WordInfo> demo) {
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

    private void sumResultsWithPlusWords(HashMap<String, WordInfo> allCandidates, List<WordInfo> candidates) {
        for (WordInfo candidate : candidates) {
            allCandidates.put(candidate.getFileName(), candidate);
        }
    }

    private void handlePlusWords(HashMap<String, WordInfo> allCandidates) {
        for (String plusWord : searchingExpression.getPlusWords()) {
            for (WordInfo wordInfo : searchForAWord(plusWord)) {
                allCandidates.put(wordInfo.getFileName(), wordInfo);
            }
        }
    }

    private void printResults(List<WordInfo> candidates) {
        if (candidates == null || candidates.size() == 0) {
            System.out.println(ConsoleColors.ANSI_RED + "please try a different keyword for your search!"
                    + ConsoleColors.ANSI_RESET);
            return;
        }
        for (WordInfo candidate : candidates)
            System.out.println("File name: " + ConsoleColors.ANSI_CYAN + candidate.getFileName() + ConsoleColors.ANSI_RESET
                    + " ApproximatePosition: " + ConsoleColors.ANSI_GREEN + (candidate.getPosition() - candidates.size() + 1)
                    + ConsoleColors.ANSI_RESET);
    }


    private List<WordInfo> searchForAWord(String word) {
        word = word.toLowerCase();
        return invertedIndex.getIndex().get(word);
    }

    private void deleteMinusWordsFromCandidates(List<WordInfo> candidates) {
        for (String minusWord : searchingExpression.getMinusWords()) {
            List<WordInfo> toBeRemovedDocs = searchForAWord(minusWord);
            for (WordInfo toBeRemovedDoc : toBeRemovedDocs)
                for (int j = candidates.size() - 1; j >= 0; j--)
                    if (candidates.get(j).getFileName().equals(toBeRemovedDoc.getFileName()))
                        candidates.remove(j);
        }
    }

}