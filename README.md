# NewsService
News Service to aggregate news from multiple sources and show them based on custom rules

# News Class & NewsCategory Enum
    public class News
    {
        public string Heading { get; set; }    
        public string Content { get; set; }
        public bool IsPriority { get; set; }
        public NewsCategory Category { get; set; }
    }

    public enum NewsCategory
    {
        Political,
        Sports,
        Travel,
        Advertisements
    }

    Priority news - IsPriority field set to true
    Normal news - IsPriority field set to false AND category not set to Advertisements
    Ads - IsPriority field set to false AND category set to Advertisements

# Aggregation Strategy
News Service aggregates news data from multiple registered sources and return a single aggregated list. The PriorityNewsFirstAggregationStrategy strategy implementation puts priority news across all sources first followed by normal and ads.

# Paging Strategy
While Aggregation Strategy is responsible to pull data from multiple sources and returning a single list, paging stretegy defines how those aggregated News data would be served in pages. Paging strategy honors pageSize and accordingly arranges data in multiple pages based on the custom strategy. The PriorityNewsFirstWithWeightedNewsPagingStrategy implementation puts priority news always at the top followed by normal news and ads but it also makes sure that news (priority or normal) vs ads ratio is at max 3 : 1.

# Endpoints:

# Getting news:
    GET     https://{host}
    GET     https://{host}/api/news
    GET     https://{host}?pageNumber=1&pageSize=4
    GET     https://{host}/api/news?pageNumber=1&pageSize=4
        
# News Source Registration:
    POST    https://{host}/api/newssource?sourceKey=InternalNews
    
# Adding news to a new source:
    PUT     https://{host}/api/newssource/1
    Body:
    [
        {
            "Heading": "P1",
            "Content": "priority news 1",
            "Category": "Political",
            "IsPriority": true
        },
        {
            "Heading": "P2",
            "Content": "priority news 2",
            "Category": "Political",
            "IsPriority": true
        },
        {
            "Heading": "N1",
            "Content": "normal news 1",
            "Category": "Sports",
            "IsPriority": false
        },
        {
            "Heading": "A1",
            "Content": "Ad 1",
            "Category": "Advertisements",
            "IsPriority": false
        }
    ]
    
# Unregister a news source:
    DEL     https://{host}/api/newssource?sourceKey=InternalNews
    
