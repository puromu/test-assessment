DROP TABLE IF EXISTS assessment_choices;
DROP TABLE IF EXISTS assessment_results;
DROP TABLE IF EXISTS assessment_questions;

CREATE TABLE assessment_questions (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    question_text TEXT NOT NULL,
    correct_choice_id INT NOT NULL
);

CREATE TABLE assessment_choices (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    question_id INT NOT NULL,
    choice_text TEXT NOT NULL,
    CONSTRAINT fk_assessment_choices_question
        FOREIGN KEY (question_id)
        REFERENCES assessment_questions(id)
        ON DELETE CASCADE
);

CREATE TABLE assessment_results (
    id INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    full_name VARCHAR(200) NOT NULL,
    score INT NOT NULL,
    total INT NOT NULL,
    submitted_at TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO assessment_questions (
    question_text,
    correct_choice_id
)
OVERRIDING SYSTEM VALUE
VALUES
(1, 'ผลลัพธ์ของ 3 + 6 เท่ากับข้อใด', 3),
(2, 'x + 2 = 4 จงหาค่า x', 6);

INSERT INTO assessment_choices (
    id,
    question_id,
    choice_text
)
OVERRIDING SYSTEM VALUE
VALUES
(1, 1, '3'),
(2, 1, '5'),
(3, 1, '9'),
(4, 1, '11'),
(5, 2, '1'),
(6, 2, '2'),
(7, 2, '3'),
(8, 2, '4');