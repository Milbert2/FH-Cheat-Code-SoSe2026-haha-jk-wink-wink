export async function getScores(orderby) {

    const response = await fetch("list.php?orderby=" + orderby);
    const scores = await response.json();
    return scores;
}