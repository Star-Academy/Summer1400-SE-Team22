import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.util.List;


class SearcherTest {
    private static InvertedIndex invertedIndex = new InvertedIndex();
    private static Searcher searcher = new Searcher();
    static {
        invertedIndex = new InvertedIndex();
        searcher = new Searcher();
        invertedIndex.indexAllFiles("src/test/java/TestResources/EnglishData");
        searcher.setInvertedIndex(invertedIndex);
    }
    @Test
    void normalWordSearch(){
        List<WordInfo> result = searcher.search("ali");
       Assertions.assertEquals(7, result.size());
       int i = 1;
        for (WordInfo wordInfo : result) {
            Assertions.assertEquals(i + "", wordInfo.getFileName());
            i++;
        }
    }

    @Test
    void normalWordsSearch(){
        List<WordInfo> result = searcher.search("ali va hasan godratmand");
        Assertions.assertEquals(1, result.size());
        Assertions.assertEquals("3", result.get(0).getFileName());
    }
    @Test
    void deleteMinusWordsFromEmptyResultTest() {
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();
        Assertions.assertDoesNotThrow(() -> searcher.search("-kid"));
        assert(outContent.toString().contains("please try a different keyword for your search!"));
    }
}