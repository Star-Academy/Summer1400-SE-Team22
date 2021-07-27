import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;


class SearcherTest {
    @Test
    void deleteMinusWordsFromEmptyResultTest() {
        InvertedIndex invertedIndex = new InvertedIndex();
        invertedIndex.indexAllFiles("src/main/resources/SampleEnglishData/EnglishData");
        Searcher.setInvertedIndex(invertedIndex);
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();
        Assertions.assertDoesNotThrow(() -> Searcher.search("-kid"));

        assert(outContent.toString().contains("please try a different keyword for your search!"));
    }
}