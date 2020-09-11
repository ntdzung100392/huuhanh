import React from 'react';
import StoreLocatorMap from './StoreLocatorMap';
import StoreLocatorList from './storeLocatorList';
import StoreLocatorApi from 'apis/StoreLocatorApi';
import PropTypes from 'prop-types';

export default class StoreLocatorContainer extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      storesList: {},
      apiKey: '',
      countryCode: '',
      defaultCoordinates: '',
      maxStore: 15,
      zoomLevel: 11,
      postalCode: 0,
      mapIsReady: false,
      currentCoordinates: {},
      map: {}
    };

    this.callMapService = this.callMapService.bind(this);
    this.searchStores = this.searchStores.bind(this);
    this.initMap = this.initMap.bind(this);
    this.getCurrentPosition = this.getCurrentPosition.bind(this);
    this.detectUserCurrentLocation = this.detectUserCurrentLocation.bind(this);
    this.initInfoWindow = this.initInfoWindow.bind(this);
    this.clearMarkers = this.clearMarkers.bind(this);
    this.registerAutoSuggestionForPlaceName = this.registerAutoSuggestionForPlaceName.bind(this);
    this.zoomIntoCertainArea = this.zoomIntoCertainArea.bind(this);
    this.getAutocompletePredictions = this.getAutocompletePredictions.bind(this);
    this.getAutocompleteRequest = this.getAutocompleteRequest.bind(this);
    this.getPlaceDetails = this.getPlaceDetails.bind(this);
    this.setLocationMarker = this.setLocationMarker.bind(this);
    this.onSubmitValue = this.onSubmitValue.bind(this);
  }

  componentDidMount() {
    StoreLocatorApi.getMapSettings(this.props.contentId)
      .then(({ data }) => {
        this.setState({
          apiKey: data.apiKey,
          maxStore: data.maxStoresShown,
          defaultCoordinates: data.defaultCoordinates,
          countryCode: data.defaultCountryCode,
          zoomLevel: data.defaultZoomLevel
        }, () => {
          this.callMapService();
        });
      });
  }

  callMapService() {
    const script = document.createElement('script');
    const APIKey = this.state.apiKey;
    const url = `https://maps.googleapis.com/maps/api/js?key=` + APIKey + `&libraries=places`;

    script.src = url;
    script.async = true;
    script.defer = true;
    script.addEventListener('load', () => {
      this.setState({ mapIsReady: true }, () => {
        this.initMap();
      });
    });

    document.body.appendChild(script);
  }

  zoomIntoCertainArea(originLocation) {
    this.state.map.setCenter(originLocation);
    this.state.map.setZoom(this.state.zoomLevel);
  }

  registerAutoSuggestionForPlaceName(autocomplete) {
    autocomplete.addListener('place_changed', async () => {
      const place = autocomplete.getPlace();
      if (place && place.geometry) {
        const postalCode = parseInt(place.name);
        if (this.state.postalCode === postalCode)
          return

        this.setState({ postalCode: postalCode });

        const originLocation = place.geometry.location;
        this.setLocationMarker(originLocation);
        this.searchStores(originLocation.lat(), originLocation.lng(), this.state.maxStore);
        this.zoomIntoCertainArea(originLocation);
      }
    });
  }

  onSubmitValue() {
    const input = document.getElementById('search-box');
    const postalCode = parseInt(input.value);

    if (postalCode && this.state.postalCode !== postalCode) {
      this.setState({ postalCode: postalCode });
      this.getAutocompletePredictions(postalCode);
      return;
    }
  }

  getAutocompletePredictions(inputValue) {
    if (inputValue) {
      const autocompleteService = new window.google.maps.places.AutocompleteService();
      const placeRequest = this.getAutocompleteRequest(inputValue);
      autocompleteService.getPlacePredictions(placeRequest, (predictionsArr) => {
        if (predictionsArr) {
          const validPlace = Object.values(predictionsArr)[0];
          if (validPlace && parseInt(validPlace.terms[2].value) === inputValue) {
            this.getPlaceDetails(validPlace);
            return;
          }
        }

        const defaultLatitude = parseFloat(this.state.defaultCoordinates.split(",")[0]);
        const defaultLongitude = parseFloat(this.state.defaultCoordinates.split(",")[1]);
        this.state.map.setCenter({ lat: defaultLatitude, lng: defaultLongitude });

        this.state.map.setZoom(this.state.zoomLevel);
        this.setState({ storesList: {} });
        this.clearMarkers(this.state.map);
      });
    }
  }

  getPlaceDetails(place) {
    const placeDetailsRequest = { placeId: place.place_id, fields: ['name', 'geometry'] };
    const placesService = new window.google.maps.places.PlacesService(this.state.map);
    placesService.getDetails(placeDetailsRequest, (placeDetailsResult) => {
      if (placeDetailsResult.geometry) {
        const originLocation = placeDetailsResult.geometry.location;
        this.searchStores(originLocation.lat(), originLocation.lng(), this.state.maxStore);
        this.zoomIntoCertainArea(originLocation);
      }
    });
  }

  getAutocompleteRequest(inputValue) {
    return { input: inputValue, componentRestrictions: { country: this.state.countryCode }, types: ['(regions)'] };
  }

  initMap() {
    const defaultLatitude = parseFloat(this.state.defaultCoordinates.split(",")[0]);
    const defaultLongitude = parseFloat(this.state.defaultCoordinates.split(",")[1]);

    const map = new window.google.maps.Map(document.getElementById('map'), {
      zoom: this.state.zoomLevel,
      center: { lat: defaultLatitude, lng: defaultLongitude },
      styles: [
        {
          "elementType": "geometry",
          "stylers": [
            {
              "color": "#f5f5f5"
            }
          ]
        },
        {
          "elementType": "labels.icon",
          "stylers": [
            {
              "visibility": "off"
            }
          ]
        },
        {
          "elementType": "labels.text.fill",
          "stylers": [
            {
              "color": "#616161"
            }
          ]
        },
        {
          "elementType": "labels.text.stroke",
          "stylers": [
            {
              "color": "#f5f5f5"
            }
          ]
        },
        {
          "featureType": "administrative.land_parcel",
          "elementType": "labels.text.fill",
          "stylers": [
            {
              "color": "#bdbdbd"
            }
          ]
        },
        {
          "featureType": "poi",
          "elementType": "geometry",
          "stylers": [
            {
              "color": "#eeeeee"
            }
          ]
        },
        {
          "featureType": "poi",
          "elementType": "labels.text.fill",
          "stylers": [
            {
              "color": "#757575"
            }
          ]
        },
        {
          "featureType": "poi.park",
          "elementType": "geometry",
          "stylers": [
            {
              "color": "#e5e5e5"
            }
          ]
        },
        {
          "featureType": "poi.park",
          "elementType": "labels.text.fill",
          "stylers": [
            {
              "color": "#9e9e9e"
            }
          ]
        },
        {
          "featureType": "road",
          "elementType": "geometry",
          "stylers": [
            {
              "color": "#ffffff"
            }
          ]
        },
        {
          "featureType": "road.arterial",
          "elementType": "labels.text.fill",
          "stylers": [
            {
              "color": "#757575"
            }
          ]
        },
        {
          "featureType": "road.highway",
          "elementType": "geometry",
          "stylers": [
            {
              "color": "#dadada"
            }
          ]
        },
        {
          "featureType": "road.highway",
          "elementType": "labels.text.fill",
          "stylers": [
            {
              "color": "#616161"
            }
          ]
        },
        {
          "featureType": "road.local",
          "elementType": "labels.text.fill",
          "stylers": [
            {
              "color": "#9e9e9e"
            }
          ]
        },
        {
          "featureType": "transit.line",
          "elementType": "geometry",
          "stylers": [
            {
              "color": "#e5e5e5"
            }
          ]
        },
        {
          "featureType": "transit.station",
          "elementType": "geometry",
          "stylers": [
            {
              "color": "#eeeeee"
            }
          ]
        },
        {
          "featureType": "water",
          "elementType": "geometry",
          "stylers": [
            {
              "color": "#c9c9c9"
            }
          ]
        },
        {
          "featureType": "water",
          "elementType": "labels.text.fill",
          "stylers": [
            {
              "color": "#9e9e9e"
            }
          ]
        }
      ]
    });

    const icon = {
      url: '/images/location-solid-60x66.svg',
      scaledSize: new google.maps.Size(60, 66),
      size: new google.maps.Size(60, 66)
    };

    map.data.setStyle({
      icon: icon
    });

    this.setState({ map: map }, () => {
      const infoWindow = new window.google.maps.InfoWindow();
      this.initInfoWindow(infoWindow);
      this.initAutocomplete();

      this.detectUserCurrentLocation(map);
    });
  }

  initAutocomplete() {
    const input = document.getElementById('search-box');
    const options = {
      types: ['(regions)'],
      componentRestrictions: { country: this.state.countryCode }
    };

    const autocomplete = new window.google.maps.places.Autocomplete(input, options);
    autocomplete.setFields(
      ['address_components', 'geometry', 'name']);
    this.registerAutoSuggestionForPlaceName(autocomplete, this.state.map);
  }

  initInfoWindow(infoWindow) {
    this.state.map.data.addListener('click', (event) => {
      const name = event.feature.getProperty('name');
      const phone = event.feature.getProperty('address').phone;
      const position = event.feature.getGeometry().get();
      const address = event.feature.getProperty('address').formattedAddress;
      const content = `
          <div class="find-store-marker-container">
            <h4 class="find-store__store-name">${name}</h4>
            <ul class="find-store__store-address">
              <li class="is-flex align-items-center">
                <i class="icon-location-solid"></i>
                ${address}
              </li>
              <li class="is-flex align-items-center">
                <i class="icon-phone"></i>
                <a href="tel:${phone}">${phone ? phone : 'Not Available'}</a>
              </li>
            </ul>
        </div>
        `;
      infoWindow.setContent(content);
      infoWindow.setPosition(position);
      infoWindow.setOptions({ pixelOffset: new window.google.maps.Size(0, -30) });
      infoWindow.open(this.state.map);
    });
  }

  getCurrentPosition(map) {
    return new Promise((resolve) => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(async function (position) {
          const pos = {
            lat: position.coords.latitude,
            lng: position.coords.longitude
          };
          map.setCenter(pos);
          resolve(pos);
        });
      }
    });
  }

  async detectUserCurrentLocation(map) {
    const currentPos = await this.getCurrentPosition(map);
    this.setState({
      currentCoordinates: currentPos
    }, () => {
      this.searchStores(this.state.currentCoordinates.lat, this.state.currentCoordinates.lng, this.state.maxStore);
    });
  }

  clearMarkers(map) {
    this.state.map.data.forEach(function (feature) {
      map.data.remove(feature);
    });
  }

  searchStores(lat, long, maxStore) {
    StoreLocatorApi.searchNearbyStores(lat, long, this.props.contentId, maxStore)
      .then(({ data }) => {
        this.setState({ storesList: data }, () => {
          this.clearMarkers(this.state.map);
          if (this.state.storesList.features.length > 0) {
            const firstStoreCoordinates = data.features[0].geometry.coordinates;
            const latitude = parseFloat(firstStoreCoordinates[1]);
            const longitude = parseFloat(firstStoreCoordinates[0]);
            this.state.map.setCenter({ lat: latitude, lng: longitude });

            if (this.state.storesList.features.length === 1) {
              this.state.map.setZoom(20);
            } else {
              this.state.map.setZoom(this.state.zoomLevel);
            }

            this.state.map.data.addGeoJson(data);
          }
        });
      });
  }

  setLocationMarker(location) {
    const originMarker = new window.google.maps.Marker(this.state.map);
    originMarker.setPosition(location);
    originMarker.setVisible(true);
  }

  render() {
    const { storesList } = this.state;
    return (
      <div className="map-container">
        <div className="dealer-location-page">
          <div className="find-stores find-stores-stockist">
            <div className="find-store__search-bar">
              <StoreLocatorMap onSubmitValue={this.onSubmitValue} />
            </div>
            <div className="find-store__map">
              <div id="map" className="map-find-stockist"></div>
            </div>
            <div className="find-store__result find-store-stockist__result">
              <StoreLocatorList item={storesList} />
            </div>
          </div>
        </div>
      </div>
    );
  }
}

StoreLocatorContainer.propTypes = {
  contentId: PropTypes.string
};