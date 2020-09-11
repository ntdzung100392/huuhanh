import axios from 'axios';

class QuestionApi {
  static getQuestions(categoryId) {
    return axios.get(`${AppConfig.apis.getQuestionsFilterCategoryByIdUrl}/${categoryId}`);
  }

  static searchQuestionsFilterUrl(searchRequest, pageNumber) {
    pageNumber = pageNumber || 1;
    return axios.post(`${AppConfig.apis.searchQuestionsFilterUrl}?pageNumber=${pageNumber}`, searchRequest);
  }
}

export default QuestionApi;