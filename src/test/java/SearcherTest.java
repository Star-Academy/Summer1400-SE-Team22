import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;


class SearcherTest {
    private static InvertedIndex invertedIndex = new InvertedIndex();
    private static Searcher searcher = new Searcher();
    static {
        invertedIndex = new InvertedIndex();
        searcher = new Searcher();
        invertedIndex.indexAllFiles("src/test/java/TestResources/EnglishData");
    }
    @Test
    void normalWordsSearch(){

    }
    @Test
    void deleteMinusWordsFromEmptyResultTest() {
        searcher.setInvertedIndex(invertedIndex);
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();
        Assertions.assertDoesNotThrow(() -> searcher.search("-kid"));

        assert(outContent.toString().contains("please try a different keyword for your search!"));
    }
}