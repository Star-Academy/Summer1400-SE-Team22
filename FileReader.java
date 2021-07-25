import java.io.File;
import java.io.FileNotFoundException;
import java.util.List;
import java.util.Scanner;

public class FileReader {

    public static String readFileContent(File file) {
        StringBuilder output = new StringBuilder();
        try {
            Scanner scanner = new Scanner(file);
            while (scanner.hasNextLine()) {
                String data = scanner.nextLine();
                output.append(data);
            }
            scanner.close();
        } catch (FileNotFoundException e) {
            return null;
        }
        return output.toString();
    }

//    List<WordInfo> demo = searchForAWord(words[i]);//mnop opfg jkls
//            for (int j = candidates.size() - 1; j >= 0; j--) {
//        WordInfo candidate = candidates.get(j);
//        boolean hasDocWord = false;
//        for (WordInfo wordInfo : demo) {
//            if (!wordInfo.fileName.equals(candidate.fileName)) continue;
//            hasDocWord = true;
//            if (candidate.position + ignoredCounter + 1 != wordInfo.position)
//                candidates.remove(candidate);
//        }
//        if (!hasDocWord) try {
//            candidates.remove(candidate);
//        } catch (Exception ignored) {
//        }
//    }
}
