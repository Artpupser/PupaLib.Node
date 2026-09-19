#!/bin/bash

OUTPUT="all_code.cs"
ROOT="."

> "$OUTPUT" # очистить файл

find "$ROOT" -type f -name "*.cs" | while read -r file; do
    echo "// ===== FILE: $file =====" >> "$OUTPUT"
    cat "$file" >> "$OUTPUT"
    echo -e "\n" >> "$OUTPUT"
done

echo "Done! All .cs files merged into $OUTPUT"