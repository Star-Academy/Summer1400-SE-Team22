import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.List;


class SearcherTest {

    private static InvertedIndex invertedIndex = new InvertedIndex();
    private static Searcher searcher = new Searcher();

    @BeforeEach
    void init() {
        invertedIndex = new InvertedIndex();
        searcher = new Searcher();
        invertedIndex.indexAllFiles("src/test/java/TestResources/EnglishData");
        Searcher.setInvertedIndex(invertedIndex);
    }

    @Test
    void normalWordSearch() {
        List<WordInfo> result = null;
        try {
            result = searcher.search("ali");
        } catch (Exception ignored) {
        }
        Assertions.assertEquals(7, result.size());
        int i = 1;
        for (WordInfo wordInfo : result) {
            Assertions.assertEquals(i + "", wordInfo.getFileName());
            i++;
        }
    }

    @Test
    void normalWordsSearch() {
        List<WordInfo> result = null;
        try {
            result = searcher.search("ali va hasan godratmand");
        } catch (Exception ignored) {
        }
        Assertions.assertEquals(1, result.size());
        Assertions.assertEquals("3", result.get(0).getFileName());
    }

    @Test
    void normalWordsWithStopWordsBetweenSearch() {
        List<WordInfo> result = null;
        try {
            result = searcher.search("ali va hasan is godratmand");
        } catch (Exception ignored) {
        }
        Assertions.assertEquals(1, result.size());
        Assertions.assertEquals("4", result.get(0).getFileName());
    }

    @Test
    void consoleColorInstantiation() {
        new ConsoleColors();
    }


    @Test
    void plusWordsSearchOne() {
        List<WordInfo> result = null;
        try {
            result = searcher.search("+ali va hasan godratmand");
        } catch (Exception ignored) {
        }
        Assertions.assertEquals(7, result.size());
    }

    @Test
    void plusWordsSearchTwo() {
        List<WordInfo> result = null;
        try {
            result = searcher.search("ali va +hasan godratmand");
        } catch (Exception ignored) {
        }
        Assertions.assertEquals(5, result.size());
    }

    @Test
    void minusWordSearch() {
        List<WordInfo> result = null;
        try {
            result = searcher.search("ali -hasan");
        } catch (Exception ignored) {
        }
        Assertions.assertEquals(2, result.size());
    }

    @Test
    void stopWordSearch() {
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();
        List<WordInfo> result = null;
        try {
            result = searcher.search("is");
        } catch (Exception ignored) {
        }
        Assertions.assertEquals(null, result);
        assert (outContent.toString().contains("please try a different keyword for your search!"));
    }

    @Test
    void stopWordsSearch() {
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();
        List<WordInfo> result = null;
        try {
            result = searcher.search("were is");
        } catch (Exception ignored) {
        }
        Assertions.assertEquals(null, result);
        assert (outContent.toString().contains("please try a different keyword for your search!"));
    }

    @Test
    void notExistWordsSearch() throws ClassNotFoundException, NoSuchMethodException, InvocationTargetException, IllegalAccessException {
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();
        List<WordInfo> result = null;
        try {
            result = searcher.search("inkalamehvojoodnadaradaziz");
        } catch (Exception ignored) {
        }
        Method printResultMethod = Class.forName("Searcher").getDeclaredMethod("printResults", List.class);
        printResultMethod.setAccessible(true);
        printResultMethod.invoke(searcher, result);
        Assertions.assertEquals(null, result);
        assert (outContent.toString().contains("there is no match!"));
    }

    @Test
    void deleteMinusWordsFromEmptyResultTest() {
        ByteArrayOutputStream outContent = new ByteArrayOutputStream();
        System.setOut(new PrintStream(outContent));
        outContent.reset();
        Assertions.assertDoesNotThrow(() -> searcher.search("-kid"));
        assert (outContent.toString().contains("please try a different keyword for your search!"));
    }
}