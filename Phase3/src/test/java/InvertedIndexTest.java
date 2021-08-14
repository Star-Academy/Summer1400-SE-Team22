import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class InvertedIndexTest {

    InvertedIndex invertedIndex;

    @BeforeEach
    void initialize() {
        invertedIndex = new InvertedIndex();
    }

    @Test
    void indexAllFilesTest() {
        assertDoesNotThrow(() ->
                invertedIndex.indexAllFiles("src/main/resources/SampleEnglishData/EnglishData"));
        assertNotNull(invertedIndex.getIndex());
    }

    @Test
    void getStopWordsTest() {
        assertEquals(InvertedIndex.getStopWords().size(), 119);
    }

}