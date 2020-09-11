import React from 'react';

class CenterActionLink extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <div className="page__view-all">
        <a type="button" href="#" role="button" onClick={this.props.onClick}>
          <span className="button__text">{this.props.title} </span>
          <i className="icon-arrow-right-circle" alt="icon-next" />
        </a>
      </div>
    );
  }
}

export default CenterActionLink;