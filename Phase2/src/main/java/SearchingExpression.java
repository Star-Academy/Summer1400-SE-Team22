import java.util.Arrays;
import java.util.LinkedList;
import java.util.List;

public class SearchingExpression {
    private List<String> words;
    private List<String> plusWords;
    private List<String> minusWords;

    public SearchingExpression(String searchingExpression) {
        setWords(new LinkedList<>(Arrays.asList(searchingExpression.toLowerCase().split("\\s+"))));
        setPlusWords(new LinkedList<>());
        setMinusWords(new LinkedList<>());

        isolatePlusAndMinusWords();
    }

    public List<String> getWords() {
        return words;
    }

    public void setWords(List<String> words) {
        this.words = words;
    }

    public List<String> getPlusWords() {
        return plusWords;
    }

    public void setPlusWords(List<String> plusWords) {
        this.plusWords = plusWords;
    }

    public List<String> getMinusWords() {
        return minusWords;
    }

    public void setMinusWords(List<String> minusWords) {
        this.minusWords = minusWords;
    }

    private void isolatePlusAndMinusWords() {
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
}
